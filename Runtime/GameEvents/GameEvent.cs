using System.Collections.Generic;
using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(GameEvent), menuName = "GameEvents/"+nameof(GameEvent), order=-1)]
    public class GameEvent : ScriptableObject
    {
        private HashSet<IEventListener> listeners = new HashSet<IEventListener>();

        public void RegisterListener(IEventListener listener) => listeners.Add(listener);
        public void UnregisterListener(IEventListener listener) => listeners.Remove(listener);


        public void Raise()
        {
            foreach (var listener in listeners)
                listener.RaiseEvent();
        }
    }
}
