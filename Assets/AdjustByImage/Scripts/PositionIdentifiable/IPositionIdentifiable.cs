using UnityEngine;

namespace AdjustByImage.Scripts.PositionIdentifiable
{
    /// <summary>
    ///     写真の名前を入れると、写真に対応したARオブジェクトのローカル座標とクォータニオンを返す
    /// </summary>
    internal interface IArObjectIdentifiable
    {
        (bool, Vector3, Quaternion) GetLocalPositionByName(string imageName);
    }
}