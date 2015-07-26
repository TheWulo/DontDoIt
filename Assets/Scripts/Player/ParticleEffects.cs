using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ParticleEffects : MonoBehaviour
    {
        public ParticleSystem BloodParticleSystem;

        public ParticleSystem DustParticleSystem;

        private Rigidbody2D playerRigidbody2D;
        private PlayerMovement playerMovement;

        public void PlayBlood()
        {
            BloodParticleSystem.Play();
        }

        private void Start()
        {
            playerRigidbody2D = GetComponent<Rigidbody2D>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (playerRigidbody2D.velocity.x > 0.1f || playerRigidbody2D.velocity.x < 0.1f && playerMovement.touchingGround)
            {
                if (!DustParticleSystem.isPlaying)
                {
                    DustParticleSystem.Play();
                }
            }
            else
            {
                if (DustParticleSystem.isPlaying)
                {
                    DustParticleSystem.Stop();
                }
            }
        }
    }
}
