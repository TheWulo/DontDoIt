using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Net : BulletBase
    {
        public Vector3 bulletSpawningDirection;
        public float firePower;
        private bool velocitySet = false;

        void FixedUpdate()
        {
            if (!isServer || velocitySet)
                return;
            transform.position += Time.deltaTime * bulletSpawningDirection * firePower;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerBase>().Die(DeathReason.Net);
            }
        }
    }
}
