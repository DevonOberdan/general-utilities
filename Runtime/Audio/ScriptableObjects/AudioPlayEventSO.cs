using System;
using UnityEngine;

namespace FinishOne.GeneralUtilities.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioPlayEventSO), menuName = "Audio/" + nameof(AudioPlayEventSO), order = 0)]
    public class AudioPlayEventSO : ScriptableObject
    {
        public Action<AudioConfigSO> OnRequestAudio;

        public void Raise(AudioConfigSO audioConfig)
        {
            OnRequestAudio?.Invoke(audioConfig);
        }
    }
}
