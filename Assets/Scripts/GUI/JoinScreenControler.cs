using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class JoinScreenControler : GUIScreen
    {
        NetworkClient myClient;

        public InputField IPfield;

        public void Connect()
        {
            myClient = new NetworkClient();
            myClient.RegisterHandler(MsgType.Connect, OnConnected);
            myClient.Connect(IPfield.text, 7777);
        }

        public void OnConnected(NetworkMessage netMsg)
        {
            GUIManager.instance.HideCurrentScreen();
        }
    }
}
