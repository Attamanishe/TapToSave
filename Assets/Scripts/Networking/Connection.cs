using System;
using UnityEngine;

public class Connection : MonoBehaviour
{
    public void Init(string localhost, Action onConnected)
    {
        Debug.Log(string.Format("Connected to {0}", localhost));
        onConnected.Invoke();
    }
}