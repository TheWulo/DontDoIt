using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Net : BulletBase
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerBase>().Die(DeathReason.Net);
            }
        }
    }
}
