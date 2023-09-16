using UnityEngine;
using UnityEngine.Events;

namespace FinishOne.GeneralUtilities
{
    public class TriggerEvent : MonoBehaviour
    {
        public UnityEvent TriggerEnterEvent;
        public UnityEvent TriggerExitEvent;

        private void OnTriggerEnter(Collider other) => TriggerEnterEvent.Invoke();
        private void OnTriggerExit(Collider other) => TriggerExitEvent.Invoke();
    }
}