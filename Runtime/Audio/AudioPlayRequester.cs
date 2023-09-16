using UnityEngine;

namespace FinishOne.GeneralUtilities.Audio
{
    public class AudioPlayRequester : MonoBehaviour
    {
        [SerializeField] private AudioConfigSO defaultAudio;
        [SerializeField] private AudioPlayEventSO audioPlayEvent;

        public void Request(AudioConfigSO requestedConfig = null)
        {
            if (requestedConfig != null)
                audioPlayEvent.Raise(requestedConfig);
            else
                audioPlayEvent.Raise(defaultAudio);
        }
    }
}