using AdjustByImage.Scripts.PositionContainer;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    /// <summary>
    /// PositionContainerInScriptの動作テスト
    /// enum ImageIdsに密結合しているので注意
    /// </summary>
    public class PositionContainerInScriptTest
    {
        /// <summary>
        /// ArObjectPositionContainerInScript.GetLocalPositionByNameが期待した値を返してくるかテスト
        /// </summary>
        [Test]
        public void ArObjectPositionContainerInScriptTest()
        {
            var arObjectPositionContainerInScript = new ObjectPositionContainerInScript();
            Assert.AreEqual((true, new Vector3(1, 1, 0), Quaternion.Euler(1, 1, 0)),
                arObjectPositionContainerInScript.GetLocalPositionByName("Rafflesia"));
            Assert.AreEqual((true, new Vector3(1, 1, 1),Quaternion.Euler(1, 1, 0)),
                arObjectPositionContainerInScript.GetLocalPositionByName("unitylogowhiteonblack"));
            Assert.AreEqual((true, new Vector3(-1, 1, 0),Quaternion.Euler(1, 1, 0)),
                arObjectPositionContainerInScript.GetLocalPositionByName("QRCode"));
        }

        [Test]
        public void DifferenceTest()
        {
            var arObjectPositionContainerInScript = new ObjectPositionContainerInScript();
            Assert.AreEqual(new Vector3(0, 0, 0),
                new Vector3(1, 1, 0) - arObjectPositionContainerInScript.GetLocalPositionByName("Rafflesia").Item2);
            Assert.AreEqual(new Vector3(0, 0, 0),
                new Vector3(1, 1, 1) - arObjectPositionContainerInScript.GetLocalPositionByName("unitylogowhiteonblack")
                    .Item2);
            Assert.AreEqual(new Vector3(0, 0, 0),
                new Vector3(-1, 1, 0) - arObjectPositionContainerInScript.GetLocalPositionByName("QRCode").Item2);
        }
        
        [Test]
        public void 失敗するテスト()
        {
            var arObjectPositionContainerInScript = new ObjectPositionContainerInScript();
            Assert.AreEqual(new Vector3(1, 1, 0),
                new Vector3(1, 1, 0) - arObjectPositionContainerInScript.GetLocalPositionByName("Rafflesia").Item2);
            Assert.AreEqual(new Vector3(1, 0, 0),
                new Vector3(0, 0, 0) - arObjectPositionContainerInScript.GetLocalPositionByName("unitylogowhiteonblack")
                    .Item2);
            Assert.AreEqual(new Vector3(0, 1, 1),
                new Vector3(-1, 1, 0) - arObjectPositionContainerInScript.GetLocalPositionByName("QRCode").Item2);
        }
    }
}
