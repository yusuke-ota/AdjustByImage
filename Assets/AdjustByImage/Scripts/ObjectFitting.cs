using System.Threading.Tasks;
using AdjustByImage.Scripts.Calculator;
using AdjustByImage.Scripts.PositionIdentifiable;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace AdjustByImage.Scripts
{
    public class ObjectFitting : MonoBehaviour
    {
        private IArObjectIdentifiable arObjectIdentify;
        private readonly ICalculatetable calculator = new Calculator.Calculator();

        private void Start()
        {
            arObjectIdentify = GetComponent<IArObjectIdentifiable>();
        }

        /// <summary>
        ///     画像を認識すると同時にmove_arObjectsを行うと、更新前のため
        ///     aRTrackedImage.transform.positionの取得がうまくいかない。
        ///     少し時間をおいてmove_arObjectsを行う
        /// </summary>
        /// <param name="aRObjects">動かしたいオブジェクト群</param>
        /// <param name="aRTrackedImage">認識している写真</param>
        public void AlignObjectsParallel(GameObject aRObjects, ARTrackedImage aRTrackedImage)
        {
            Task.Run(() =>
            {
                Task.Delay(50);
                AlignObjectsLowFrequency(aRObjects, aRTrackedImage);
            });
        }

        /// <summary>
        ///     認識した写真に応じて、ARObjectを動かす
        ///     低頻度向け(2回/secとか)
        /// </summary>
        /// <param name="aRObjects">動かしたいオブジェクト群</param>
        /// <param name="aRTrackedImage">認識している写真</param>
        public void AlignObjectsLowFrequency(GameObject aRObjects, ARTrackedImage aRTrackedImage)
        {
            // 画像名から、対応する子オブジェクトの座標、クォータニオンを取得する
            var (success, childLocalPosition, childLocalRotation) =
                arObjectIdentify.GetLocalPositionByName(aRTrackedImage.referenceImage.name);
            // GetLocalPositionByNameに失敗するとfalseが返ってくるので、その場合は何もしない
            if (!success) return;

            // 回転計算
            var setRotation = calculator.CalculateSpin(aRObjects.transform.rotation, aRTrackedImage.transform.rotation,
                childLocalRotation);
            aRObjects.transform.rotation = setRotation;

            // 座標計算
            var movement =
                calculator.CalculateMovement(aRTrackedImage.transform.position, childLocalPosition, setRotation);
            aRObjects.transform.position = movement;
        }

        /// <summary>
        ///     認識した写真に応じて、ARObjectを動かす
        ///     高頻度で動かすために、線形補完機能される
        /// </summary>
        /// <param name="aRObjects"></param>
        /// <param name="aRTrackedImage"></param>
        /// <param name="t"></param>
        /// <param name="slearp"></param>
        public void AlignObjectsHighFrequency(GameObject aRObjects, ARTrackedImage aRTrackedImage, float t, bool slearp)
        {
            // 画像名から、対応する子オブジェクトの座標、クォータニオンを取得する
            var (success, childLocalPosition, childLocalRotation) =
                arObjectIdentify.GetLocalPositionByName(aRTrackedImage.referenceImage.name);
            // GetLocalPositionByNameに失敗するとfalseが返ってくるので、その場合は何もしない
            if (!success) return;

            // 回転計算
            var aRObjectRotation = aRObjects.transform.rotation;
            var setRotation =
                calculator.CalculateSpin(aRObjectRotation, aRTrackedImage.transform.rotation, childLocalRotation);

            setRotation = slearp
                ? Quaternion.Slerp(setRotation, aRObjectRotation, t)
                : Quaternion.Lerp(setRotation, aRObjectRotation, t);
            aRObjects.transform.rotation = setRotation;

            // 座標計算
            var movement =
                calculator.CalculateMovement(aRTrackedImage.transform.position, childLocalPosition, setRotation);
            movement = Vector3.Lerp(movement, aRObjects.transform.position, t); // 線形補完
            aRObjects.transform.position = movement;
        }

        #region UnUsed

        // /// <summary>
        // /// 認識した写真に応じて、SessionOriginを動かす
        // /// todo: 修正
        // /// </summary>
        // /// <param name="aRSessionOrigin">動かしたいARSessionOrigin</param>
        // /// <param name="aRObjects">基準となるオブジェクト</param>
        // /// <param name="aRTrackedImage">認識している写真</param>
        // private　void AdjustWithMakeContentAppearAt(ARSessionOrigin aRSessionOrigin, GameObject aRObjects, ARTrackedImage aRTrackedImage)
        // {
        //     // aRObjectの子オブジェクトと写真の位置をあわせるので、aRObjectの子オブジェクトのローカル座標分オフセットする
        //     var (success, offset, childLocalRotation) = arObjectIdentify.GetLocalPositionByName(aRTrackedImage.referenceImage.name);
        //     // GetLocalPositionByNameに失敗するとfalseが返ってくる
        //     if (!success) return;
        //
        //     // 回転計算
        //     var setRotation = CalculateSpin(aRObjects, aRTrackedImage, childLocalRotation);
        //
        //     // Vector3は値型なのでコピーされる
        //     // aRObjectPos　と　aRSessionOrigin.transform.localPosition　は別物
        //     var adjustPos = aRTrackedImage.transform.position -　setRotation * offset;
        //     aRSessionOrigin.MakeContentAppearAt(aRObjects.transform, adjustPos, setRotation);
        // }

        #endregion
    }
}