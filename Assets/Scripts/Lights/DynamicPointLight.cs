using UnityEngine;

namespace Assets.Scripts.Lights
{
    public class DynamicPointLight : MonoBehaviour
    {
        public Light PointLight;

        public float blinkingSpeed;

        private bool increasing;
        private bool decreasing;

        private void Start()
        {
            increasing = true;
            decreasing = false;
        }

        private void Update()
        {
            if (increasing)
            {
                PointLight.intensity += blinkingSpeed*Time.deltaTime;
                if (PointLight.intensity >= 8)
                {
                    increasing = false;
                    decreasing = true;
                }
            }
            if (decreasing)
            {
                PointLight.intensity -= blinkingSpeed * Time.deltaTime;
                if (PointLight.intensity <= 4)
                {
                    increasing = true;
                    decreasing = false;
                }
            }
        }
            
        }
}
