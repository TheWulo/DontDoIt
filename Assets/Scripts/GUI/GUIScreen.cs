using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public enum ScreenType { MainMenu = 0, Join = 1}

    public class GUIScreen : MonoBehaviour
    {
        [SerializeField] 
        private Canvas targetCanvas;

        public ScreenType type;

        private void Awake()
        {
            if (targetCanvas != null)
                targetCanvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            targetCanvas.enabled = true;
        }

        public void Hide()
        {
            targetCanvas.enabled = false;
        }
    }
}
