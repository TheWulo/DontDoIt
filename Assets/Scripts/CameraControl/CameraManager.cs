using UnityEngine;

namespace Assets.Scripts.CameraControl
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager instance;

        public UnityEngine.Camera mainCamera;

        private void Awake()
        {
            instance = this;
        }


    }
}
