using UnityEngine;

namespace Assets.Scripts.Audio
{
    public enum AudioType { PlayerDie }

    public class AudioClip : MonoBehaviour
    {
        public AudioType type;
        [SerializeField]
        private AudioSource clip;

        private void Start()
        {
            if (clip == null)
                clip = GetComponent<AudioSource>();
        }

        public void Play()
        {
            clip.Play();
        }
    }
}
