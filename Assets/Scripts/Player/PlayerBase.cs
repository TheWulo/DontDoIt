using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.CameraControl;
using Assets.Scripts.Spawners;
using UnityEngine;
using AudioType = Assets.Scripts.Audio.AudioType;

namespace Assets.Scripts.Player
{
    public enum Team
    {
        Suicidials, Rescuers
    }

    public enum DeathReason
    {
        Trap, Net
    }

    public class PlayerBase : MonoBehaviour
    {
        public string Name;
        public Color Color;
        public Team Team;

        public delegate void OnDeathHandler(PlayerBase player, DeathReason type);

        public event OnDeathHandler OnDeath;

        void Start()
        {
            Team = TeamManager.instance.Register(this);
            CameraManager.instance.mainCamera.GetComponent<CameraMovement>().SubscribePlayer(this);
        }


        public void Die(DeathReason reason)
        {
            OnDeath(this, reason);
            gameObject.transform.position = RespawnManager.instance.GetSpawnPoint();
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
        }
    }
}
