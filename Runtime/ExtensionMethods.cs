using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public static class ExtensionMethods
    {
        public static Vector3 NewX(this Vector3 vec, float newVal) => new(newVal, vec.y, vec.z);
        public static Vector3 NewY(this Vector3 vec, float newVal) => new(vec.x, newVal, vec.z);
        public static Vector3 NewZ(this Vector3 vec, float newVal) => new(vec.x, vec.y, newVal);

        public static bool Between(this float index, float min, float max) => index >= min && index <= max;
        public static bool Between(this int index, int min, int max) => index >= min && index <= max;


        public static int Mod(this int a, int b) => (a % b + b) % b;

        public static bool IsValidIndex<T>(this IEnumerable<T> list, int index) => index.Between(0, list.Count() - 1);

        public static T RandomItem<T>(this List<T> list) => list.ToArray().RandomItem();
        public static T RandomItem<T>(this T[] array) => array[Random.Range(0, array.Count())];
    }
}