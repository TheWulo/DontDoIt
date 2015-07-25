using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.CameraControl;
using Assets.Scripts.Spawners;
using UnityEngine;
using AudioType = Assets.Scripts.Audio.AudioType;

namespace Assets.Scripts.Player
{

    public class PlayerBase : MonoBehaviour
    {
        public string Name;
        public Color Color;

        private void Start()
        {
            CameraManager.instance.mainCamera.GetComponent<CameraMovement>().SubscribePlayer(this);
        }

        public void Die()
        {
            gameObject.transform.position = RespawnManager.instance.GetSpawnPoint();
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
        }
    }
}
