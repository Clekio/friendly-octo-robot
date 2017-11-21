using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestroyExtraCopyOfGameobject : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != this.gameObject)
        {
            Destroy(gameObject);
        }
    }
}