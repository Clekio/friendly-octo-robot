using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ActivadorCamaraFija : MonoBehaviour
{
    GameObject camara;

    GameObject triggerPlaneo;

    private void Start()
    {
        camara = GameObject.Find("Camera3");

        triggerPlaneo = GameObject.Find("QuitarPlaneo");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camara.GetComponent<BoxCollider2D>().enabled = true;

            triggerPlaneo.GetComponent<BoxCollider2D>().enabled =  false;
        }
    }
}