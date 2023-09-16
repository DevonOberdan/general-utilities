using UnityEngine;

namespace FinishOne.GeneralUtilities.Audio
{
    /* As you make more and more of these, if patterns emerge, like one shot type sound effects
     * all having virtually the same Config settings, then maybe make an inherited class that
     * has those defaults, like:
     * 
     * PickupAudioConfigSO
     *      or
     * OneShotAudioConfigSO
     * 
     * first will probably be a OneShot one and a Music one
     */
    [CreateAssetMenu(fileName = nameof(AudioConfigSO), menuName = "Audio/" + nameof(AudioConfigSO), order = 0)]
    public class AudioConfigSO : ScriptableObject
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private bool oneShot;
        [SerializeField] private bool randomPitch;

        public AudioClip Clip => clip;
        public bool OneShot => oneShot;
        public bool RandomPitch => randomPitch;

    }
}
