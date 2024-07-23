using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public static class Vector3ExtensionMethods
    {
        public static Vector3 NewX(this Vector3 vec, float newVal) => new(newVal, vec.y, vec.z);
        public static Vector3 NewY(this Vector3 vec, float newVal) => new(vec.x, newVal, vec.z);
        public static Vector3 NewZ(this Vector3 vec, float newVal) => new(vec.x, vec.y, newVal);

        public static Vector3 WithNew(this Vector3 vec, float? x=null, float? y=null, float? z = null)
        {
            return new(x ?? vec.x, y ?? vec.y, z ?? vec.z);
        }
    }
}
