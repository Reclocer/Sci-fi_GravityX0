using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions 
{
    public static IEnumerator DoAfterSeconds(this MonoBehaviour monoBehaviour, Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}
