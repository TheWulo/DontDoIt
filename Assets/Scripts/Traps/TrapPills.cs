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
            if (player)
            {
                Debug.Log("Arggh");
                player.RpcDelayedDie((int)KillDelay, DeathReason.TrapSpike);
                //Spawner.PillCollected = true;
            }
        }
    }
}
