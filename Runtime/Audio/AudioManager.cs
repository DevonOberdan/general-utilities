using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FinishOne.GeneralUtilities.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer master;

        [SerializeField] private Vector2 pitchRange = new Vector2(0.8f, 1.2f);
        [SerializeField] private AudioPlayEventSO sfxPlayEvent, musicPlayEvent;

        private float defaultSFXPitch;
        private const string MASTER_GROUP = "Master";
        private const string MUSIC_GROUP = "Music";
        private const string SFX_GROUP = "SFX";
        private const string VOLUME_SUFFIX = "Volume";
        private AudioSource sfxSource, musicSource;

        //TODO: Implement audio pooling
        private List<GameObject> soundSourcePool;
        private GameObject soundSourceFactory;

        private void Awake()
        {
            transform.GetChild(0).TryGetComponent(out sfxSource);
            transform.GetChild(1).TryGetComponent(out musicSource);

            defaultSFXPitch = sfxSource.pitch;
        }

        private void OnEnable()
        {
            EventManager.AddListener<AudioSettingsChangedEvent>(evt => UpdateVolumeSettings(evt.changedSettings));
            sfxPlayEvent.OnRequestAudio += CreateSound;
            musicPlayEvent.OnRequestAudio += ChangeTrack;
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<AudioSettingsChangedEvent>(evt => UpdateVolumeSettings(evt.changedSettings));
            sfxPlayEvent.OnRequestAudio -= CreateSound;
            musicPlayEvent.OnRequestAudio -= ChangeTrack;
        }

        private void UpdateVolumeSettings(SettingsAudio settings)
        {
            SetVolume(MASTER_GROUP + VOLUME_SUFFIX, settings.MasterVolume / 100f);
            SetVolume(MUSIC_GROUP + VOLUME_SUFFIX, settings.MusicVolume / 100f);
            SetVolume(SFX_GROUP + VOLUME_SUFFIX, settings.SfxVolume / 100f);
        }

        private void SetVolume(string mixerGroup, float linearValue)
        {
            if (master == null)
                return;

            float decibelVal = GetDecibelValueFromLinear(linearValue);
            master.SetFloat(mixerGroup, decibelVal);
        }

        private float GetDecibelValueFromLinear(float linearValue) => linearValue == 0 ? -144f : 20.0f * Mathf.Log10(linearValue);

        private IEnumerator ResetPitch(float time)
        {
            yield return new WaitForSeconds(time + 0.05f);
            if (!sfxSource.isPlaying)
                sfxSource.pitch = defaultSFXPitch;
        }

        private void ChangeTrack(AudioConfigSO newTrack)
        {
            musicSource.Stop();
            musicSource.clip = newTrack.Clip;
            musicSource.Play();
        }

        private void CreateSound(AudioConfigSO audioToPlay)
        {
            if (audioToPlay == null)
                return;

            float pitch = audioToPlay.RandomPitch ? Random.Range(pitchRange.x, pitchRange.y) : defaultSFXPitch;

            bool differentPitch = sfxSource.pitch != pitch;
            sfxSource.pitch = pitch;

            if (audioToPlay.OneShot)
            {
                sfxSource.PlayOneShot(audioToPlay.Clip, 1);
            }
            else
            {
                sfxSource.clip = audioToPlay.Clip;
                sfxSource.Play();
            }

            if (differentPitch)
            {
                StopAllCoroutines();
                StartCoroutine(ResetPitch(audioToPlay.Clip.length));
            }
        }
    }
}