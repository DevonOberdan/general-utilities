using UnityEngine;
using UnityEngine.Events;

namespace FinishOne.GeneralUtilities
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;

        [SerializeField] bool registerOnEnable;

        private void Awake() => RegisterSelf();
        private void OnDestroy() => UnregisterSelf();


        private void OnEnable()
        {
            if(registerOnEnable)
                RegisterSelf();
        }
        private void OnDisable()
        {
            if(registerOnEnable)
                UnregisterSelf();
        }


        private void RegisterSelf() 
        {
            if(gameEvent != null)
                gameEvent.Register(this);
        }
        private void UnregisterSelf()
        {
            if(gameEvent != null)   
                gameEvent.Unregister(this);
        }

        public void RaiseEvent() => response?.Invoke();
    }
}