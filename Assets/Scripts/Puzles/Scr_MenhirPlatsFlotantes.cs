using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MenhirPlatsFlotantes : MonoBehaviour
{
    public static bool activarPlataformas;

    void OnTriggerEnter2D(Collider2D other)
    {
        activarPlataformas = true;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -5);
    }
}