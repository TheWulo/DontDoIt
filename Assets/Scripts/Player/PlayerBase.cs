using Assets.Scripts.Audio;
using Assets.Scripts.Camera;
using Assets.Scripts.CameraControl;
using Assets.Scripts.Spawners;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using AudioType = Assets.Scripts.Audio.AudioType;

namespace Assets.Scripts.Player
{
    public enum Team
    {
        Suicidials, Rescuers, None
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
        [SyncVar(hook = "OnIsDeadChange")]
        public bool IsDead;

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
            Die(reason);
        }

        [ClientRpc]
        public void RpcDelayedDie(int seconds, DeathReason reason)
        {

            StartCoroutine(DelayedDie(seconds, reason));
        }

        private IEnumerator DelayedDie(int seconds, DeathReason reason)
        {
            yield return new WaitForSeconds(seconds);
            Die(reason);
        }

        private void Die(DeathReason reason)
        {
            if (reason == DeathReason.Net && Team != Team.Rescuers)
                TeamManager.instance.CmdAddScoreForRescuers(1);
            else if (Team != Team.Rescuers)
                TeamManager.instance.CmdAddScoreForSuicidas(1);

            if (reason == DeathReason.TrapSpike)
            {
                GetComponent<ParticleEffects>().PlayBlood();
            }

            Debug.Log(string.Format("RpcDie: PlayerId: {0}, Reason: {1}, Team: {2}", netId, reason, Team));
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
            if (OnDeath != null)
            {
                OnDeath(this, reason);
            }
        }

        public void OnIsDeadChange(bool newVal)
        {
            Debug.Log("OnIsDead!!!!");
            if (Team != Team.Rescuers)
                TeamManager.instance.CmdAddScoreForSuicidas(1);

            Debug.Log(string.Format("RpcDie: PlayerId: {0}, Reason: {1}, Team: {2}", netId, DeathReason.TrapSpike, Team));
            AudioManager.instance.PlayAudio(AudioType.PlayerDie);
            if (OnDeath != null)
            {
                OnDeath(this, DeathReason.TrapSpike);
            }
            IsDead = false;
        }

        #region server side client logic
        [ClientRpc]
        public void RpcSetTeam(string teamName, uint playerId)
        {
            if (netId.Value != playerId)
                return;
            Team = (Team)Enum.Parse(typeof(Team), teamName);
            Debug.Log("Got team " + Team.ToString() + " Player Id: " + playerId);
            var weapon = GetComponent<PlayerWeapon>();
            if (weapon && Team == Team.Suicidials)
            {
                Destroy(weapon);
            }
            //SetSkin();
        }

        public void SetSkin()
        {
            switch (Team)
            {
                case Team.Suicidials:
                    GameObject skin = Instantiate(TeamManager.instance.GetRandomSueciderSkin());
                    skin.transform.parent = gameObject.transform;
                    skin.transform.localPosition = new Vector3(0, 0 - skin.GetComponent<SkinOffset>().offset, 0); //Because fuck you.
                    skin.transform.localScale = new Vector3(0.8f, 0.4f, 1); //Because fuck you even more.
                    GetComponent<AnimationController>().GFXAnimator = skin.GetComponent<Animator>();
                    GetComponent<AnimationController>().GFXObject = skin;
                    break;
                case Team.Rescuers:
                    GameObject skin2 = Instantiate(TeamManager.instance.GetRandomRescuerSkin());
                    skin2.transform.parent = gameObject.transform;
                    skin2.transform.localPosition = new Vector3(0, 0 - skin2.GetComponent<SkinOffset>().offset, 0); //Because fuck you.
                    skin2.transform.localScale = new Vector3(0.8f, 0.4f, 1); //Because fuck you even more.
                    GetComponent<AnimationController>().GFXAnimator = skin2.GetComponent<Animator>();
                    GetComponent<AnimationController>().GFXObject = skin2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
