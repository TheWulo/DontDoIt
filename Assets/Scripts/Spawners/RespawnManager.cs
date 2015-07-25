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
            player.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(SpawnPlayer(player, SpawnTimeInSeconds));
        }

        IEnumerator SpawnPlayer(PlayerBase player, float time)
        {
            yield return new WaitForSeconds(time);
            var availablePositions = player.Team == Team.Suicidials ? SuicidialSpawnPoints : RescuerSpawnPoints;
            var length = availablePositions.Count;
            var newPosition = availablePositions[Random.Range(0, length)].position;
            player.transform.position = newPosition;
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
