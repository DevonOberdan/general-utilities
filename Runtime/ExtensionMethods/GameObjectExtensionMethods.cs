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

        public static bool TryGetColor(this GameObject gameObject, out Color color)
        {
            Graphic graphic = gameObject.GetComponent<Graphic>();
            Material mat = null;
            if (graphic == null && gameObject.TryGetComponent(out MeshRenderer rend))
            {
                mat = rend.material;
            }

            if (graphic != null)
            {
                color = graphic.color;
                return true;
            }
            else if (mat != null)
            {
                color = mat.color;
                return true;
            }

            Debug.LogError("Trying to GetColor on an object with no visual component.");
            color = Color.white;
            return false;
        }

        public static void SetColor(this GameObject gameObject, Color color)
        {
            Graphic graphic = gameObject.GetComponent<Graphic>();
            Material mat = null;

            if (graphic == null && gameObject.TryGetComponent(out MeshRenderer rend))
            {
                mat = rend.material;
            }

            if (graphic != null)
            {
                graphic.color = color;
            }
            else if (mat != null)
            {
                mat.color = color;
            }
            else
            {
                Debug.LogError("Trying to SetColor on an object with no visual component.");
            }
        }
    }
}
