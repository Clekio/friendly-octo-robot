using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Menhir : MonoBehaviour
{
    public GameObject menhir;
    public static bool activarTentaculos;

    void OnTriggerEnter2D(Collider2D other)
    {
        activarTentaculos = true;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -5);
    }
}