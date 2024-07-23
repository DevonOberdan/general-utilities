using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public static class ColorExtensionMethods
    {
        public static Color AtNewAlpha(this Color color, float newAlpha) => new Color(color.r, color.g, color.b, newAlpha);

        public static Color DarkenedToPercent(this Color color, float perc)
        {
            float h, s, v;

            Color.RGBToHSV(color, out h, out s, out v);
            return Color.HSVToRGB(h, s, perc);
        }

        public static Color DarkenedByPercent(this Color color, float perc)
        {
            float h, s, v;

            Color.RGBToHSV(color, out h, out s, out v);
            return Color.HSVToRGB(h, s, v*perc);
        }
    }
}
