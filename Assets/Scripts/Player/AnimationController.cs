using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public enum AnimationDeathType { DieSpike }

    public class AnimationController : MonoBehaviour
    {

        public GameObject GFXObject;
        public Animator GFXAnimator;

        public void Update()
        {
            if (Input.GetAxis("Horizontal") >= 0.1)
            {
                if (GFXObject.gameObject.transform.localScale.x > 0)
                    GFXObject.gameObject.transform.localScale = new Vector3(GFXObject.gameObject.transform.localScale.x * -1, GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
            }
            if (Input.GetAxis("Horizontal") <= -0.1)
            {
                if (GFXObject.gameObject.transform.localScale.x < 0)
                    GFXObject.gameObject.transform.localScale = new Vector3(GFXObject.gameObject.transform.localScale.x * -1, GFXObject.gameObject.transform.localScale.y, GFXObject.gameObject.transform.localScale.z);
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
            GFXAnimator.Play(type.ToString());
        }
    }
}
