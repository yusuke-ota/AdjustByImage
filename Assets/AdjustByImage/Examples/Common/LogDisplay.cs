using UnityEngine;
using UnityEngine.UI;

namespace AdjustByImage.Examples.Common
{
    public class LogDisplay : MonoBehaviour
    {
        public Text message = null;

        private void Awake()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void HandleLog(string logText, string stackTrace, LogType type)
        {
            message.text = logText;
        }
    }
}