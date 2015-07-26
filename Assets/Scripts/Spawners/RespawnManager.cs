using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Spawners
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager instance;
        public int SpawnTimeInSeconds;

        public List<Transform> SuicidialSpawnPoints;
        public List<Transform> RescuerSpawnPoints;

        private void Awake()
        {
            instance = this;
        }

        public void Register(PlayerBase player)
        {
            player.OnDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath(PlayerBase player, DeathReason type)
        {
            player.GetComponent<PlayerMovement>().alive = false;
            StartCoroutine(SpawnPlayer(player, SpawnTimeInSeconds, type));
        }

        IEnumerator SpawnPlayer(PlayerBase player, float time, DeathReason type)
        {
            yield return new WaitForSeconds(time);
            var availablePositions = player.Team == Team.Suicidials ? SuicidialSpawnPoints : RescuerSpawnPoints;
            var length = availablePositions.Count;
            var newPosition = availablePositions[Random.Range(0, length)].position;
            if (type == DeathReason.Net)
            {
                var weapon = GameObject.FindObjectOfType<PlayerWeapon>();
                weapon.CmdSpawnBullet(player.transform.position, new Vector2());
            }
            player.transform.position = newPosition;
            player.GetComponent<PlayerMovement>().alive = true;
            
        }
    }
}
