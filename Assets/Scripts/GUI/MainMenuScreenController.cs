namespace Assets.Scripts.GUI
{
    public class MainMenuScreenController : GUIScreen
    {
        public NetworkClientWrapper NetworkClientWrapper;

        private void HostServer()
        {
            NetworkClientWrapper.StartServerOnly();
            GUIManager.instance.ShowScreen(ScreenType.GameGUI);
        }

        private void ConnectLocalClient()
        {
            NetworkClientWrapper.StartClient();
            GUIManager.instance.ShowScreen(ScreenType.GameGUI);
        }

        public void HostAndConnect()
        {
            NetworkClientWrapper.StartHost();
            GUIManager.instance.ShowScreen(ScreenType.GameGUI);
        }
    }
}
