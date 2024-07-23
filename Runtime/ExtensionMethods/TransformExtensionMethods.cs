using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public static class TransformExtensionMethods
    {
        /// <summary>
        /// Resets transform's position, scale and rotation
        /// </summary>
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void DestroyChildren(this Transform parent)
        {
            parent.ForEachChild(child => Object.Destroy(child));
        }

        public static void DestroyChildrenImmediate(this Transform parent)
        {
            parent.ForEachChild(child => Object.DestroyImmediate(child));
        }

        /// <summary>
        /// Iterates over all children of the parent Transform and performs the passed in Action on each of them
        /// </summary>
        /// <param name="action">Pass in an Action (read: child => child.SetActive(true), etc.)</param>
        public static void ForEachChild(this Transform parent, System.Action<Transform> action)
        {
            for (int i = parent.childCount-1;  i >= 0; i--)
            {
                action(parent.GetChild(i));
            }
        }
    }
}