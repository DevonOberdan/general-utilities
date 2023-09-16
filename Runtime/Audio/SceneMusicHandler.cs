using UnityEngine;

namespace FinishOne.GeneralUtilities.Audio
{
    public class SceneMusicHandler : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<AudioPlayRequester>().Request();
        }
    }
}