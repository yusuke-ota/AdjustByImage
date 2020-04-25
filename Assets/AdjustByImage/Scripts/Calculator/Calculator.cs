using UnityEngine;

namespace AdjustByImage.Scripts.Calculator
{
    public struct Calculator : ICalculatetable
    {
        /// <summary>
        /// 子オブジェクトと目的の角度の差を計算する
        /// </summary>
        /// <param name="setRotation">角度合わせしたい親オブジェクトのRotation</param>
        /// <param name="targetRotation">角度合わせしたい目的のRotation</param>
        /// <param name="childLocalRotation">角度合わせしたい子オブジェクトのLocalRotation</param>
        /// <returns>子オブジェクトと目的の角度の差</returns>
        public Quaternion CalculateSpin(Quaternion setRotation, Quaternion targetRotation,
            Quaternion childLocalRotation)
        {
            var childRotation = setRotation * childLocalRotation;

            // 子オブジェクトの角度とある角度の差を計算
            // TargetRot = ChildRot * Buffer
            // =>  Buffer = ChildRot^(-1) * TargetRot
            // 親オブジェクトをその分回転させる
            // Rotation * Buffer = Rotation * ChildRot^(-1) * TargetRot
            return targetRotation * Quaternion.Inverse(childRotation) * setRotation;
        }

        /// <summary>
        /// 子オブジェクトと目的の位置の差を計算する
        /// </summary>
        /// <param name="targetPosition">位置合わせしたい目的の座標</param>
        /// <param name="childLocalPosition">位置合わせしたい子オブジェクトのローカル座標</param>
        /// <param name="rotation">子オブジェクトのローカル座標に加えたい角度</param>
        /// <returns>子オブジェクトと目的の位置の差</returns>
        public Vector3 CalculateMovement(Vector3 targetPosition, Vector3 childLocalPosition, Quaternion rotation)
        {
            // rotation * childLocalPosition = 子オブジェクトの transform.position
            return targetPosition - rotation * childLocalPosition;
        }
    }
}