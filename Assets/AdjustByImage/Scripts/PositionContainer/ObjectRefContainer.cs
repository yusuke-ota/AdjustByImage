using System.Collections.Generic;
using AdjustByImage.Scripts.PositionIdentifiable;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace AdjustByImage.Scripts.PositionContainer
{
    /// <summary>
    ///     ルートにあるAR用オブジェクトにある、写真と対応するゲームオブジェクトを管理するクラス
    ///     認識した写真の座標に合わせてほしい、AR用オブジェクトの座標を返す
    /// </summary>
    public class ObjectRefContainer : MonoBehaviour, IArObjectIdentifiable
    {
        [SerializeField] [Tooltip("使いたいTrackedImageが設定されているReferenceImageLibraryを設定してください")]
        private XRReferenceImageLibrary xrReferenceImageLibrary;

        [SerializeField] [Tooltip("使いたいTrackedImageの対になるArObject内の子オブジェクトを設定してください")]
        private List<GameObject> refLocalObjects;

        #region Values

        private bool successUnion;
        private GameObject arObjectChildUnion;

        #endregion

        private readonly Dictionary<string, GameObject> stringToGameObject = new Dictionary<string, GameObject>();

        private void Start()
        {
            for (var i = 0; i < xrReferenceImageLibrary.count; i++)
            {
                if (i > refLocalObjects.Count) continue;
                stringToGameObject.Add(xrReferenceImageLibrary[i].name, refLocalObjects[i]);
            }
        }

        public (bool, Vector3, Quaternion) GetLocalPositionByName(string imageName)
        {
            if (imageName == null) return (false, Vector3.zero, Quaternion.identity);

            successUnion = stringToGameObject.TryGetValue(imageName, out arObjectChildUnion);

            var (vector3, quaternion) = successUnion
                ? (arObjectChildUnion.transform.localPosition, arObjectChildUnion.transform.localRotation)
                : (Vector3.zero, Quaternion.identity);

            return (successUnion, vector3, quaternion);
        }
    }
}