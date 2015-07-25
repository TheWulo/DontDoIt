using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class JoinScreenControler : GUIScreen
    {
        public NetworkClientWrapper NetworkClientWrapper;

        public InputField IPfield;

        void Start()
        {
            IPfield.text = NetworkClientWrapper.DefaultAddres;

            NetworkClientWrapper.OnConnectionEstablished += OnConnected;
        }

        public void Connect()
        {
            NetworkClientWrapper.NetworkAddress = IPfield.text;
            NetworkClientWrapper.StartClient();
            GUIManager.instance.ShowScreen(ScreenType.GameGUI);
        }

        public void OnConnected(NetworkMessage netMsg)
        {
            GUIManager.instance.ShowScreen(ScreenType.GameGUI);
            Debug.Log(netMsg.reader);
        }
    }
}
