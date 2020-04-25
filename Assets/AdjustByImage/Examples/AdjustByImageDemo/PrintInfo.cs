using UnityEngine;
using UnityEngine.UI;

namespace AdjustByImage.Examples.AdjustByImageDemo
{
    public class PrintInfo : MonoBehaviour
    {
        private GameObject arObjects;
        private GameObject stone;
        private GameObject block;
        private Text textArea;

        private Transform arObjectsTransform;
        private Transform stoneTransform;
        private Transform blockTransform;
        
        // Start is called before the first frame update
        private void Start()
        {
            arObjects = GameObject.Find("AR Objects");
            stone = GameObject.Find("Stone");
            block = GameObject.Find("Block");
            textArea = GetComponent<Text>();
            
            // transformは重いらしいので、前もって、参照を得ておく
            arObjectsTransform = arObjects.transform;
            stoneTransform = stone.transform;
            blockTransform = block.transform;
        }

        // Update is called once per frame
        private void Update()
        {
            var arObjectsPos = arObjectsTransform.position;
            var stonePos = stoneTransform.position;
            var blockPos = blockTransform.position;
            textArea.text =
                $"AR Objects:\nx:{arObjectsPos.x}\ny:{arObjectsPos.y}\nz:{arObjectsPos.z}\nStone:\nx:{stonePos.x}\ny:{stonePos.y}\nz:{stonePos.z}\nQRCode:\nx:{blockPos.x}\ny:{blockPos.y}\nz:{blockPos.z}";
        }
    }
}