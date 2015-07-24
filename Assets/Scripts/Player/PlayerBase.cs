using Assets.Scripts.Audio;
using Assets.Scripts.Spawners;
using UnityEngine;
using AudioType = Assets.Scripts.Audio.AudioType;

namespace Assets.Scripts.Player
{

    public class PlayerBase : MonoBehaviour
    {
        public string Name;
        public Color Color;

        public void Die()
        {
            gameObject.transform.position = RespawnManager.instance.GetSpawnPoint();
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
        }
    }
}
