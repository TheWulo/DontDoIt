using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.CameraControl;
using Assets.Scripts.Spawners;
using System;
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

        [SyncVar]
        public Team Team;

        public delegate void OnDeathHandler(PlayerBase player, DeathReason type);

        public event OnDeathHandler OnDeath;

        void Start()
        {
            if (isLocalPlayer)
            {
                CmdRegister(this.netId.Value);

                CameraManager.instance.mainCamera.GetComponent<CameraMovement>().SubscribePlayer(this);
                RespawnManager.instance.Register(this);
            }
        }

        [ClientRpc]
        public void RpcDie(DeathReason reason)
        {
            if (reason == DeathReason.Net && Team != Team.Rescuers)
                TeamManager.instance.CmdAddScoreForRescuers(1);
            else if (reason == DeathReason.Net )
                return;
            else
                TeamManager.instance.CmdAddScoreForSuicidas(1);
            Debug.Log(string.Format("RpcDie: PlayerId: {0}, Reason: {1}, Team: {2}", netId, reason, Team));
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
            if (OnDeath != null)
            {
                OnDeath(this, reason);
            }
        }

        #region server side client logic
        [ClientRpc]
        public void RpcSetTeam(string teamName, uint playerId)
        {
            if (netId.Value != playerId)
                return;
            Team = (Team)Enum.Parse(typeof(Team), teamName);
            Debug.Log("Got team " + Team.ToString() + " Player Id: " + playerId);
        }
        #endregion

        #region server side server commands
        [Command]
        public void CmdRegister(uint playerId)
        {
            Team teamData = TeamManager.instance.GetAvailableTeam();
            if (teamData == Team.Suicidials)
                TeamManager.instance.CmdAddPlayerForSuicidas();
            else
                TeamManager.instance.CmdAddPlayerForRescuers();

            RpcSetTeam(teamData.ToString(), playerId);
        }
        #endregion
    }
}
