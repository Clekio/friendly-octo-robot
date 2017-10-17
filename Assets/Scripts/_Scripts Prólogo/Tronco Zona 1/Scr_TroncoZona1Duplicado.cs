using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TroncoZona1Duplicado : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}