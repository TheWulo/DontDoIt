using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class TrapSpike : TrapBase
    {
        public float TrapKillingTime = 1;

        private void Update()
        {
            if (lastActivationTime + TrapKillingTime > Time.time)
            {
                for (int i = 0; i < playersInsideTrap.Count; i++)
                {
                    playersInsideTrap[i].RpcDie(DeathReason.TrapSpike);
                }
                playersInsideTrap.Clear();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerBase>();
            if (player)
            {
                playersInsideTrap.Add(player);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponent<PlayerBase>();
            if (player && playersInsideTrap.Contains(player))
            {
                playersInsideTrap.Remove(player);
            }
        }
    }
}
