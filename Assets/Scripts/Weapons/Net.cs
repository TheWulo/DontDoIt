using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Weapons
{
    public class Net : BulletBase
    {
        public Animator NetAnimator;
        public float VelocityThreshold = 10;

        private bool hitGround;
        private Rigidbody2D rigidbody;

        public bool isSlowed { get; private set; }

        void Start()
        {
            if (NetAnimator == null)
            {
                NetAnimator = GetComponentInChildren<Animator>();
            }
        }

        void FixedUpdate()
        {
            if (rigidbody != null)
            {
                var velocity = rigidbody.velocity.magnitude;
                if (velocity < VelocityThreshold)
                {
                    isSlowed = true;
                    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
                }
            }
            //transform.position += Time.deltaTime * bulletSpawningDirection * firePower;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!isServer) return;
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                var player = other.GetComponent<PlayerBase>();
                if (player && player.Team == Team.Suicidials && !hitGround)
                {
                    other.gameObject.GetComponent<PlayerBase>().RpcDie(DeathReason.Net);
                }
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
            rigidbody = GetComponent<Rigidbody2D>();
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
