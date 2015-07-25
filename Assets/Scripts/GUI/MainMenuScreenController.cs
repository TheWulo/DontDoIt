
using UnityEngine.Networking;

namespace Assets.Scripts.GUI
{
    public class MainMenuScreenController : GUIScreen
    {
        public NetworkClientWrapper NetworkClientWrapper;

        private void HostServer()
        {
            NetworkClientWrapper.StartServerOnly();
            GUIManager.instance.HideCurrentScreen();
        }

        private void ConnectLocalClient()
        {
            NetworkClientWrapper.StartClient();
            GUIManager.instance.HideCurrentScreen();
        }

        public void OnConnected(NetworkMessage netMsg)
        {
            GUIManager.instance.HideCurrentScreen();
        }

        public void HostAndConnect()
        {
            NetworkClientWrapper.StartHost();
            GUIManager.instance.HideCurrentScreen();
        }
    }
}
