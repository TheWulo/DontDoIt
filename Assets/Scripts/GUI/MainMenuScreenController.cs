
using UnityEngine.Networking;

namespace Assets.Scripts.GUI
{
    public class MainMenuScreenController : GUIScreen
    {
        NetworkClient myClient;

        private void HostServer()
        {
            NetworkServer.Listen(7777);
        }

        private void ConnectLocalClient()
        {
            myClient = ClientScene.ConnectLocalServer();
            myClient.RegisterHandler(MsgType.Connect, OnConnected);
            GUIManager.instance.HideCurrentScreen();
        }

        public void OnConnected(NetworkMessage netMsg)
        {
            GUIManager.instance.HideCurrentScreen();
        }

        public void HostAndConnect()
        {
            HostServer();
            ConnectLocalClient();
        }
    }
}
