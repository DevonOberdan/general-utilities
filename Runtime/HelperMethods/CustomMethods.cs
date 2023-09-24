using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FinishOne.GeneralUtilities
{
    /// <summary>
    /// Class containing "overrides"/wrappers for existing Unity functionality.
    /// 
    /// Usage:
    /// When needing to contextually Instantiate normally at runtime or with preserved Prefab linking
    /// linking in the editor, can call CustomMethods.Instantiate(...)
    /// </summary>
    public static class CustomMethods
    {
        public static T Instantiate<T>(T original, Transform parent) where T : Object
        {
            T newObject = null;

            if (Application.isPlaying)
            {
                newObject = Object.Instantiate(original, parent);
            }
            else
            {
#if UNITY_EDITOR
                newObject = PrefabUtility.InstantiatePrefab(original, parent) as T;
#endif
            }

            return newObject;
        }
    }
}