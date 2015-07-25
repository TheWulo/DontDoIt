using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Net : BulletBase
    {
        public Vector3 bulletSpawningDirection;
        public float firePower;

        void FixedUpdate()
        {
            if (!isServer)
                return;
            transform.position += Time.deltaTime * bulletSpawningDirection * firePower;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!isServer) return;
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerBase>().RpcDie(DeathReason.Net);
            }
        }
    }
}
