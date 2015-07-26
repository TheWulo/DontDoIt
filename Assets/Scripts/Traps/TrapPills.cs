using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Traps
{
    public class TrapPills : TrapBase
    {
        [SerializeField]
        protected float KillDelay;
        [SerializeField]
        protected PillsSpawner Spawner;
        // Use this for initialization
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerBase>();
            if (player && player.isLocalPlayer)
            {
                StartCoroutine(DelayedDeath(player));
                Spawner.Collected();
            }
        }

        private IEnumerator DelayedDeath(PlayerBase playerBase)
        {
            yield return new WaitForSeconds(KillDelay);
            if(isServer)
            playerBase.RpcDie(DeathReason.TrapSpike);
        }

    }
}
