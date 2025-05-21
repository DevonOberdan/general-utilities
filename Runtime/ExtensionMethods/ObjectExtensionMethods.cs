using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FinishOne.GeneralUtilities
{
    public static class ObjectExtensionMethods
    {
            public static TObj GetPrefabFromSource<TObj>(this TObj obj) where TObj : UnityEngine.Object
            {
    #if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    return PrefabUtility.GetCorrespondingObjectFromSource(obj);
                }
    #endif

                TObj prefab = default;

                if (obj is GameObject go)
                {
                    prefab = GetPrefabFromGameObject(go) as TObj;
                }
                else if (obj is Component c)
                {
                    prefab = GetPrefabFromComponent(obj as Component) as TObj;
                }

                return prefab;
            }

            private static UnityEngine.Object GetPrefabFromGameObject(GameObject obj)
            {
                if (!obj.TryGetComponent(out PrefabContainerReference prefabContainerRef))
                {
                    Debug.LogWarning($"Object does not have a {nameof(PrefabContainerReference)} instance attached.");
                    return null;
                }

                if (prefabContainerRef.PrefabContainer == null)
                {
                    Debug.LogWarning($"{nameof(PrefabContainerReference)} instance's {nameof(PrefabContainerSO)} reference is empty.");
                    return null;
                }

                GameObject prefab = prefabContainerRef.PrefabContainer.Prefab;

                if (prefab == null)
                {
                    Debug.LogWarning($"Found {nameof(PrefabContainerSO)}, but it has no prefab assigned.");
                }

                return prefab;
            }

            private static T GetPrefabFromComponent<T>(T component) where T : Component
            {
                GameObject prefabObj = (GameObject)GetPrefabFromGameObject(component.gameObject);

                if (prefabObj == null)
                {
                    Debug.Log("GameObject prefab is null.");
                    return null;
                }

                T prefabComponent = (T)prefabObj.GetComponent(component.GetType());

                if (prefabComponent == null)
                {
                    Debug.LogWarning($"Valid prefab found ({component.gameObject.name}), but it does not contain a component of type {nameof(T)}.\n" +
                                     $"Add the desired component to the given prefab, or call " +
                                     $"TryGetPrefabAtRuntime(this Component c, out GameObject prefab) instead.");
                    return null;
                }

                return prefabComponent;
            }
        }
}
