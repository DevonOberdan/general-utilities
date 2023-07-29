using System.Collections.Generic;
using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public abstract class BaseGameEvent<TParameter> : ScriptableObject
    {
        private List<IEventListener<TParameter>> listeners = new List<IEventListener<TParameter>>();
        
        public void RegisterListener(IEventListener<TParameter> listener) => listeners.Add(listener);
        public void UnregisterListener(IEventListener<TParameter> listener) => listeners.Remove(listener);


        public void Raise(TParameter param)
        {
            foreach(var listener in listeners)
                listener.RaiseEvent(param);
        }
    }
}
