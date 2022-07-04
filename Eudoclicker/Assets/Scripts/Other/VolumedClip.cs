using System;
using UnityEngine;

namespace nsVolumedClip
{
    [Serializable]
    public class VolumedClip
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] [Range(0f, 1f)] private float _volume;

        public AudioClip AudioClip => _audioClip;
        public float Volume => _volume;
    }
}
