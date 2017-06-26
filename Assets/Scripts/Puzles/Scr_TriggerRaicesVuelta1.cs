using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRaicesVuelta1 : MonoBehaviour
{
    public static bool trigger1Activado = false;

    [SerializeField]
    Collider2D player;

    void OnTriggerEnter2D(Collider2D player)
    {
        trigger1Activado = true;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -30);
    }
}