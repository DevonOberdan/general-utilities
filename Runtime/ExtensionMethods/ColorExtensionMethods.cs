using UnityEngine;
using UnityEngine.UI;

namespace FinishOne.GeneralUtilities
{
    public static class ColorExtensionMethods
    {
        public static Color AtNewAlpha(this Color color, float newAlpha) => new(color.r, color.g, color.b, newAlpha);

        public static Color DarkenedToPercent(this Color color, float perc)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            return Color.HSVToRGB(h, s, perc);
        }

        public static Color DarkenedByPercent(this Color color, float perc)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            return Color.HSVToRGB(h, s, v*perc);
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

        public static Renderer GrabRenderer(this GameObject go)
        {
            if (!go.TryGetComponent(out Renderer renderer))
            {
                renderer = go.GetComponentInChildren<Renderer>();

                if (renderer == null)
                {
                    Debug.LogError("No Renderer in GridPiece", go);
                }
            }
            return renderer;
        }

        public static void SetColor(this Renderer renderer, Color color)
        {
            MeshRenderer mesh = renderer as MeshRenderer;
            SpriteRenderer sprite = renderer as SpriteRenderer;

            if (mesh != null)
            {
                mesh.material.color = color;
            }
            else if (sprite != null)
            {
                sprite.color = color;
            }
        }

        public static Color GetColor(this Renderer rend)
        {
            Color color = Color.white;

            MeshRenderer mesh = rend as MeshRenderer;
            SpriteRenderer sprite = rend as SpriteRenderer;

            if (mesh != null)
            {
                color = Application.isPlaying ? mesh.material.color : mesh.sharedMaterial.color;
            }
            else if (sprite != null)
            {
                color = sprite.color;
            }
            else
            {
                Debug.LogError("Color not found in GameObject " + rend.name, rend.gameObject);
            }

            return color;
        }
    }
}
