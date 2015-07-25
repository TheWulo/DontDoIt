using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class UIAnimation : MonoBehaviour
    {
        public Image targetImage;

        public List<Sprite> SpritesList;

        public float timeBetweenSprites;
        private float timer;
        private int currentSpriteIndex;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenSprites)
            {
                currentSpriteIndex++;
                if (currentSpriteIndex >= SpritesList.Count)
                    currentSpriteIndex = 0;

                targetImage.sprite = SpritesList[currentSpriteIndex];
                timer = 0;
            }
        }

    }
}
