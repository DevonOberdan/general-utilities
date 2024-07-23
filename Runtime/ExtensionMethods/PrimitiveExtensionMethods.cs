namespace FinishOne.GeneralUtilities
{
    public static class PrimitiveExtensionMethods
    {
        public static bool Between(this float index, float min, float max) => index >= min && index <= max;
        public static bool Between(this int index, int min, int max) => index >= min && index <= max;
        public static int Mod(this int a, int b) => (a % b + b) % b;
    }
}