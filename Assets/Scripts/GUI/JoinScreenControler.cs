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
            GUIManager.instance.HideCurrentScreen();
        }

        public void OnConnected(NetworkMessage netMsg)
        {
            GUIManager.instance.HideCurrentScreen();
            Debug.Log(netMsg.reader);
        }
    }
}
