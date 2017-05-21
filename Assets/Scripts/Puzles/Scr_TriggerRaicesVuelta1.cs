using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRaicesVuelta1 : MonoBehaviour
{
    public bool trigger1 = false;
    public static bool trigger2 = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        trigger1 = true;
    }

    void Update ()
    {
        if (trigger1 == true)
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -30);
            trigger2 = true;
        }
    }
}