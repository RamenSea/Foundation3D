using System;
using NaughtyAttributes;
using RamenSea.Foundation.General;
using RamenSea.Foundation.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RamenSea.Foundation3D.Components.Audio {
    /// <summary>
    /// Plays audio clips with a random pitch and volume. This is useful for creating variation while reducing the
    /// number of audio files needed.
    ///
    /// There is an option to give it a [PredictableRandom] class if you need to have its variations predictable.
    ///
    /// TODO: add a random clip feature
    /// 
    /// <example>
    /// VariationAudioSource vas = /** reference **/;
    /// vas.Play();
    /// </example>
    /// </summary>
    public class VariationAudioSource: MonoBehaviour {
        [SerializeField] public float minPitch = 1.0f;
        [SerializeField] public float maxPitch = 1.0f;
        [SerializeField] public float minVolume = 1.0f;
        [SerializeField] public float maxVolume = 1.0f;
        [SerializeField] public AudioSource audioSource;

        [NonSerialized] public PredictableRandom predictableRandom = null;
        
        /// <summary>
        /// Plays the audio source's clip
        ///
        /// Includes a button to test out the sound in the editor.
        /// </summary>
        [Button("Test Play")]
        public virtual void Play() {
            if (this.predictableRandom != null) {
                this.audioSource.pitch = this.predictableRandom.Next(this.minPitch, this.maxPitch);
                this.audioSource.volume = this.predictableRandom.Next(this.minVolume, this.maxVolume);
            } else {
                this.audioSource.pitch = Random.Range(this.minPitch, this.maxPitch);
                this.audioSource.volume = Random.Range(this.minVolume, this.maxVolume);
            }
            this.audioSource.Play();
        }


        protected virtual void OnValidate() {
            if (this.audioSource == null) {
                this.audioSource = this.gameObject.GetComponent<AudioSource>();
            }
        }
    }
}