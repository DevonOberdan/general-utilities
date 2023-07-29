using UnityEngine;
using UnityEngine.Events;

namespace FinishOne.GeneralUtilities
{
    public interface IEventListener<T>
    {
        void RaiseEvent(T parameter);
    }

    public abstract class BaseGameEventListener<TParameter, TGameEvent, TUnityEvent> : MonoBehaviour, IEventListener<TParameter>
        where TGameEvent : BaseGameEvent<TParameter>
        where TUnityEvent : UnityEvent<TParameter>
    {
        public TGameEvent gameEvent;
        public TUnityEvent response;

        [SerializeField] bool registerOnEnable;

        private void Awake() => RegisterSelf();
        private void OnDestroy() => UnregisterSelf();


        private void OnEnable()
        {
            if (registerOnEnable)
                RegisterSelf();
        }
        private void OnDisable()
        {
            if (registerOnEnable)
                UnregisterSelf();
        }


        private void RegisterSelf()
        {
            if (gameEvent != null)
                gameEvent.RegisterListener(this);
        }
        private void UnregisterSelf()
        {
            if (gameEvent != null)
                gameEvent.UnregisterListener(this);
        }

        public void RaiseEvent(TParameter param) => response?.Invoke(param);
    }
}