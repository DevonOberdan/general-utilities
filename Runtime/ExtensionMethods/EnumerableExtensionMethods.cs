using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    public static class EnumerableExtensionMethods
    {
        public static bool IsValidIndex<T>(this IEnumerable<T> list, int index) => index.Between(0, list.Count() - 1);

        public static T RandomItem<T>(this IEnumerable<T> list) => list.ElementAt(Random.Range(0, list.Count()));
    }
}