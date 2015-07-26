using UnityEngine;

namespace Assets.Scripts.Player
{
    public class ParticleEffects : MonoBehaviour
    {
        public ParticleSystem BloodParticleSystem;

        public void PlayBlood()
        {
            BloodParticleSystem.Play();
        }
    }
}
