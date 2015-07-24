using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public List<AudioClip> AudioList;

        private void Awake()
        {
            instance = this;
        }

        public void PlayAudio(AudioType type)
        {
            foreach (var audio in AudioList.Where(audio => audio.type == type))
            {
                audio.Play();
            }
        }
    }
}
