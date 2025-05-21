using UnityEngine;
using UnityEngine.UI;

namespace FinishOne.GeneralUtilities
{
    public static class GameObjectExtensionMethods
    {
        public static T GetOrAdd<T>(this GameObject go) where T : Component
        {
            if (!go.TryGetComponent(out T comp))
            {
                comp = go.AddComponent<T>();
            }

            return comp;
        }
    }
}
