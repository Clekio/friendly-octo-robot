using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_EscudoPrologo : MonoBehaviour
{
    GameObject escudo;

    public bool evento2;

    private void Start()
    {
        escudo = GameObject.Find("Escudo");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            evento2 = true;

            escudo.SetActive(false);
        }
    }
}