using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class Net : BulletBase
    {
        public Animator NetAnimator;

        private bool hitGround;

        void Start()
        {
            if (NetAnimator == null)
            {
                NetAnimator = GetComponentInChildren<Animator>();
            }
        }

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
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                if (NetAnimator != null)
                {
                    if (!hitGround)
                    {
                        NetAnimator.Play("RopeFinish");
                        hitGround = true;
                    }
                }
            }
        }

        public void Initialize(Vector2 dir, float force)
        {
            hitGround = false;
            var rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody)
            {
                rigidbody.velocity = dir * force;
            }
            if (NetAnimator != null)
            {
                NetAnimator.Play("RopeFire");
            }
        }


        [Command]
        public void CmdOrderDestroy()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
