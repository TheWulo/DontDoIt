
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class GameGuiController : GUIScreen
    {
        public NetworkClientWrapper NetworkClientWrapper;

        [SerializeField]
        protected Text SuicidersCount;
        [SerializeField]
        protected Text RescuersCount;
        [SerializeField]
        protected Text SuicidersScore;
        [SerializeField]
        protected Text RescuersScore;

        [SerializeField]
        protected Text Time;

        private void LeaveGame()
        {
            NetworkClientWrapper.StopConnection();
            //TODO: Open Lobby Scene
        }

        void Update()
        {
            SuicidersCount.text = TeamManager.instance.SuicidersCount.ToString();
            SuicidersScore.text = TeamManager.instance.SuicidersScore.ToString();
            RescuersScore.text = TeamManager.instance.RescuersScore.ToString();
            RescuersCount.text = TeamManager.instance.RescuersCount.ToString();
            var time = Math.Round(TeamManager.instance.SecondsToGameEnd, 2);
            Time.text = time.ToString("0.00");
        }
        
    }
}
