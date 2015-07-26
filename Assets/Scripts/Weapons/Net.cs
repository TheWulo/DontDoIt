using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class Net : BulletBase
    {
        void FixedUpdate()
        {
            if (!isServer)
                return;
            //transform.position += Time.deltaTime * bulletSpawningDirection * firePower;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!isServer) return;
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerBase>().RpcDie(DeathReason.Net);
            }
        }

        public void Initialize(Vector2 dir, float force)
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody)
            {
                rigidbody.velocity = dir * force;
            }
        }


        [Command]
        public void CmdOrderDestroy()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
