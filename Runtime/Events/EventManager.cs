using System;
using System.Collections.Generic;

namespace FinishOne.GeneralUtilities
{
    public class GameSystemEvent
    {
    }

    // A simple Event System that can be used for remote systems communication
    public static class EventManager
    {
        private static readonly Dictionary<Type, Action<GameSystemEvent>> s_Events = new Dictionary<Type, Action<GameSystemEvent>>();
        private static readonly Dictionary<Delegate, Action<GameSystemEvent>> s_EventLookups =
            new Dictionary<Delegate, Action<GameSystemEvent>>();

        public static void AddListener<T>(Action<T> evt) where T : GameSystemEvent
        {
            if (!s_EventLookups.ContainsKey(evt))
            {
                Action<GameSystemEvent> newAction = (e) => evt((T)e);
                s_EventLookups[evt] = newAction;

                if (s_Events.TryGetValue(typeof(T), out Action<GameSystemEvent> internalAction))
                    s_Events[typeof(T)] = internalAction += newAction;
                else
                    s_Events[typeof(T)] = newAction;
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : GameSystemEvent
        {
            if (s_EventLookups.TryGetValue(evt, out var action))
            {
                if (s_Events.TryGetValue(typeof(T), out var tempAction))
                {
                    tempAction -= action;
                    if (tempAction == null)
                        s_Events.Remove(typeof(T));
                    else
                        s_Events[typeof(T)] = tempAction;
                }

                s_EventLookups.Remove(evt);
            }
        }

        public static void Broadcast(GameSystemEvent evt)
        {
            if (s_Events.TryGetValue(evt.GetType(), out var action))
                action.Invoke(evt);
        }

        public static void Clear()
        {
            s_Events.Clear();
            s_EventLookups.Clear();
        }
    }
}