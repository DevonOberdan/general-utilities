using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public class SettingsAudio : Settings
    {
        private const float DEFAULT_VALUE = 80f;
        private float masterVolume;
        private float musicVolume;
        private float sfxVolume;

        public float MasterVolume {
            get => masterVolume;
            set {
                masterVolume = value;
                Broadcast(this);
            }
        }

        public float MusicVolume {
            get => musicVolume;
            set {
                musicVolume = value;
                Broadcast(this);
            }
        }

        public float SfxVolume {
            get => sfxVolume;
            set {
                sfxVolume = value;
                Broadcast(this);
            }
        }

        private void Broadcast(SettingsAudio changedSettings)
        {
            Events.onAudioSettingsChanged.changedSettings = changedSettings;
            EventManager.Broadcast(Events.onAudioSettingsChanged);
        }

        public override void Load()
        {
            MasterVolume = PlayerPrefs.GetFloat(nameof(MasterVolume), DEFAULT_VALUE);
            MusicVolume = PlayerPrefs.GetFloat(nameof(MusicVolume), DEFAULT_VALUE);
            SfxVolume = PlayerPrefs.GetFloat(nameof(SfxVolume), DEFAULT_VALUE);
        }

        public override void Reset()
        {
            MasterVolume = DEFAULT_VALUE;
            MusicVolume = DEFAULT_VALUE;
            SfxVolume = DEFAULT_VALUE;
            base.Reset();
        }

        public override void Save()
        {
            PlayerPrefs.SetFloat(nameof(MasterVolume), MasterVolume);
            PlayerPrefs.SetFloat(nameof(MusicVolume), MusicVolume);
            PlayerPrefs.SetFloat(nameof(SfxVolume), SfxVolume);
            base.Save();
        }
    }
}