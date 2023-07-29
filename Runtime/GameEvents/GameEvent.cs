using System.Collections.Generic;
using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(GameEvent), menuName = "GameEvents/"+nameof(GameEvent), order=-1)]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

        public void Register(GameEventListener listener) => listeners.Add(listener);
        public void Unregister(GameEventListener listener) => listeners.Remove(listener);


        public void Raise()
        {
            foreach (var listener in listeners)
                listener.RaiseEvent();
        }
    }
}
