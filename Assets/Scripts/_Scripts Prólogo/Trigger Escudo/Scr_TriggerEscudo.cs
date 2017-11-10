using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerEscudo : MonoBehaviour
{
    GameObject escudo;

    private void Start()
    {
        escudo = GameObject.Find("Escudo");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            escudo.SetActive(false);
        }
    }
}