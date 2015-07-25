using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class GUIManager : MonoBehaviour
    {
        public static GUIManager instance;

        public List<GUIScreen> GUIScreens;
        private GUIScreen currentScreen;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            ShowScreen(ScreenType.MainMenu);
        }

        public void ShowScreen(ScreenType type)
        {
            HideCurrentScreen();
            currentScreen = GUIScreens.First(screen => screen.type == type);
            GUIScreens.First(screen => screen.type == type).Show();
            Debug.Log(type.ToString());
        }

        public void ShowScreen(int index)
        {
            var targetType = (ScreenType) index;
            ShowScreen(targetType);
        }

        public void HideCurrentScreen()
        {
            if (currentScreen == null) return;
            Debug.Log("Hiding " + currentScreen.type);
            currentScreen.Hide();
            currentScreen = null;
        }
    }
}
