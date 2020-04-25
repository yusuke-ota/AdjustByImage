using System;
using AdjustByImage.Scripts.PositionIdentifiable;
using UnityEngine;

namespace AdjustByImage.Scripts.PositionContainer
{
    /// <summary>
    ///     ルートにあるAR用オブジェクトにある、写真と対応する座標を管理するクラス
    ///     認識した写真の座標に合わせてほしい、AR用オブジェクトの座標を返す
    /// </summary>
    internal class ObjectPositionContainerInScript : IArObjectIdentifiable
    {
        [Tooltip("使いたいenum ImageIdsの対になるArObject内の座標を設定してください")]
        private readonly Vector3[,] arObjectLocalPositionArray =
        {
            {new Vector3(1, 1, 0), new Vector3(1, 1, 0)},
            {new Vector3(1, 1, 1), new Vector3(1, 1, 0)},
            {new Vector3(-1, 1, 0), new Vector3(1, 1, 0)}
        };

        public (bool, Vector3, Quaternion) GetLocalPositionByName(string imageName)
        {
            var success = Enum.TryParse(imageName, out ImageIds imageIds);
            return (success,
                arObjectLocalPositionArray[(int) imageIds, 0],
                Quaternion.Euler(arObjectLocalPositionArray[(int) imageIds, 1]));
        }
    }
}