using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    public static dontDestroy singleton;

    void Awake()
    {
        if (singleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        singleton = this;

        DontDestroyOnLoad(gameObject);
    }
}
