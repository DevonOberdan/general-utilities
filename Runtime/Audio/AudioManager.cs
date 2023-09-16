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
        private float oneShotDefaultPitch;
        private const string MASTER_GROUP = "Master";
        private const string MUSIC_GROUP = "Music";
        private const string SFX_GROUP = "SFX";
        private const string VOLUME_SUFFIX = "Volume";
        private AudioSource sfxSource, musicSource;


        [SerializeField] private AudioPlayEventSO sfxPlayEvent, musicPlayEvent;
        private List<GameObject> soundSourcePool;
        private GameObject soundSourceFactory;

        private void Awake()
        {
            transform.GetChild(0).TryGetComponent(out sfxSource);
            transform.GetChild(1).TryGetComponent(out musicSource);

            oneShotDefaultPitch = sfxSource.pitch;
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

        private void OneShot(AudioClip clip, float newPitch, float volume = 1)
        {
            sfxSource.pitch = newPitch;
            sfxSource.PlayOneShot(clip, volume);
            StopAllCoroutines();
            StartCoroutine(ResetPitch(clip.length));
        }

        private void PlaySound(AudioClip clip) => OneShot(clip, oneShotDefaultPitch);
        private void PlaySoundPitched(AudioClip clip, float pitch) => OneShot(clip, pitch);
        private void PlaySoundRandomPitch(AudioClip clip) => OneShot(clip, Random.Range(pitchRange.x, pitchRange.y));

        private IEnumerator ResetPitch(float time)
        {
            yield return new WaitForSeconds(time + 0.05f);
            if (!sfxSource.isPlaying)
                sfxSource.pitch = oneShotDefaultPitch;
        }

        private void ChangeTrack(AudioConfigSO newTrack)
        {
            musicSource.Stop();
            musicSource.clip = newTrack.Clip;
            musicSource.Play();
        }

        private void CreateSound(AudioConfigSO audioToPlay)
        {
            if (audioToPlay.RandomPitch)
                PlaySoundRandomPitch(audioToPlay.Clip);
            else
                PlaySound(audioToPlay.Clip);
        }
    }
}