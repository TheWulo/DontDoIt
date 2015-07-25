using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.CameraControl;
using Assets.Scripts.Spawners;
using UnityEngine;
using UnityEngine.Networking;
using AudioType = Assets.Scripts.Audio.AudioType;

namespace Assets.Scripts.Player
{
    public enum Team
    {
        Suicidials, Rescuers
    }

    public enum DeathReason
    {
        TrapSpike, Net
    }

    public class PlayerBase : NetworkBehaviour
    {
        public string Name;
        public Color Color;
        public Team Team;

        public delegate void OnDeathHandler(PlayerBase player, DeathReason type);

        public event OnDeathHandler OnDeath;

        void Start()
        {
            Team = TeamManager.instance.Register(this);
            if (isLocalPlayer)
            {
                CameraManager.instance.mainCamera.GetComponent<CameraMovement>().SubscribePlayer(this);
                RespawnManager.instance.Register(this);
            }
        }


        public void Die(DeathReason reason)
        {
            OnDeath(this, reason);
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
        }
    }
}
