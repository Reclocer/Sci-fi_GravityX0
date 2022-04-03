using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class BaseExtensions
{

    public static double GetCurrentTime(this double time)
    {
        var epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (DateTime.UtcNow - epochStart).TotalSeconds;
    }

    /// <summary>
    /// Return list limited count the random selected items from list
    /// If list.Count <= limitCount => return list with out changes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="limitedCount"></param>
    /// <returns></returns>
    public static List<T> SelectRandomItems<T>(this List<T> list, int limitCount = 1)
    {
        if (list.Count == 0)
            return null;

        if (list.Count <= limitCount)
            return list;

        List<T> tempList = new List<T>(limitCount);
        int rd = 0;

        for (int i = 0; i < limitCount; i++)
        {
            rd = Random.Range(0, list.Count);

            if (!tempList.Contains(list[rd]))
            {
                tempList.Add(list[rd]);
            }
            else
            {
                i--;
            }
        }

        return tempList;
    }
}