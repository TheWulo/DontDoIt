using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Spawners;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerBase:MonoBehaviour
    {
        public string Name;
        public Color Color;

        public void Die()
        {
            gameObject.transform.position = RespawnManager.instance.GetSpawnPoint();
        }
    }
}
