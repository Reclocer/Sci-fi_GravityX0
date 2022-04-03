using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class ListExtensions
{
    public static List<T> Shuffle<T>(this List<T> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }
}