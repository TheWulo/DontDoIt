using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Player
{
    public enum AnimationDeathType { DieSpike }

    public class AnimationController : NetworkBehaviour
    {

        public GameObject GFXObject;
        public Animator GFXAnimator;

        private bool isDying;

        public void Update()
        {
            if (isDying) return;

            if (isLocalPlayer)
            {
                if (Input.GetAxis("Horizontal") >= 0.1)
                {
                    if (GFXObject.gameObject.transform.localScale.x > 0)
                        GFXObject.gameObject.transform.localScale =
                            new Vector3(GFXObject.gameObject.transform.localScale.x*-1,
                                GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
                }
                if (Input.GetAxis("Horizontal") <= -0.1)
                {
                    if (GFXObject.gameObject.transform.localScale.x < 0)
                        GFXObject.gameObject.transform.localScale =
                            new Vector3(GFXObject.gameObject.transform.localScale.x*-1,
                                GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
                }

                if (Input.GetAxis("Horizontal") >= 0.1 || Input.GetAxis("Horizontal") <= -0.1)
                {
                    GFXAnimator.Play("Run");
                }
                else
                {
                    GFXAnimator.Play("Idle");
                }
            }
            else
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1f)
                {
                    if (GFXObject.gameObject.transform.localScale.x > 0)
                    GFXObject.gameObject.transform.localScale =
                           new Vector3(GFXObject.gameObject.transform.localScale.x * -1,
                               GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
                }
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x < -0.1f)
                {
                    if (GFXObject.gameObject.transform.localScale.x < 0)
                        GFXObject.gameObject.transform.localScale =
                            new Vector3(GFXObject.gameObject.transform.localScale.x*-1,
                                GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
                }
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 0.1f || gameObject.GetComponent<Rigidbody2D>().velocity.x < -0.1f)
                {
                    GFXAnimator.Play("Run");
                }
                else
                {
                    GFXAnimator.Play("Idle");
                }
            }
        }

        void Start()
        {
            var player = GetComponent<PlayerBase>();
            if (player != null)
            {
                player.OnDeath += PlayerOnOnDeath;
            }
        }

        private void PlayerOnOnDeath(PlayerBase player, DeathReason type)
        {
            switch (type)
            {
                case DeathReason.TrapSpike:
                    PlayDieAnimation(AnimationDeathType.DieSpike);
                    break;
                case DeathReason.Net:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public void PlayDieAnimation(AnimationDeathType type)
        {
            isDying = true;
            GFXAnimator.Play(type.ToString());
            StartCoroutine("Respawn");
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(3);
            isDying = false;
        }
    }
}
