using System;
using System.Collections.Generic;
using AdjustByImage.Scripts.PositionIdentifiable;
using UnityEngine;

namespace AdjustByImage.Scripts.PositionContainer
{
    /// <summary>
    ///     ルートにあるAR用オブジェクトにある、写真と対応する座標を管理するクラス
    ///     認識した写真の座標に合わせてほしい、AR用オブジェクトの座標を返す
    /// </summary>
    internal class ObjectPositionContainerOnGui : MonoBehaviour, IArObjectIdentifiable
    {
        [SerializeField] [Tooltip("使いたいReferenceImageLibraryのIDの対になるARSession内での座標を設定してください")]
        private List<ArrayOfVector3> arObjectPositionList = new List<ArrayOfVector3>();

        public (bool, Vector3, Quaternion) GetLocalPositionByName(string imageName)
        {
            var success = Enum.TryParse(imageName, out ImageIds imageIds);
            return (success, arObjectPositionList[(int) imageIds][0],
                Quaternion.Euler(arObjectPositionList[(int) imageIds][1]));
        }
    }

    [Serializable]
    public class ArrayOfVector3
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 this[int i] => i == 0 ? position : rotation;
    }
}