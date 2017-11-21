using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyExtraCopyOfGameobject : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}