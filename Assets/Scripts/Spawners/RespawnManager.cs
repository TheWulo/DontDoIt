using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class RespawnManager : MonoBehaviour
    {
        public static RespawnManager instance;

        public List<Transform> SpawnPoints; 

        private void Awake()
        {
            instance = this;
        }

        public Vector3 GetSpawnPoint()
        {
            if (SpawnPoints.Count == 0) return new Vector3(0,0,0);

            var index = Random.Range(0, SpawnPoints.Count);
            return SpawnPoints[index].position;
        }
    }
}
