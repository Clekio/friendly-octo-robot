using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CamaraFija : MonoBehaviour
{
    GameObject camaraMovil;

    private void Start()
    {
        camaraMovil = GameObject.Find("Main Camera");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camaraMovil.GetComponent<Camera>().enabled = false;


            gameObject.GetComponentInChildren<Camera>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        camaraMovil.GetComponent<Camera>().enabled = true;

        gameObject.GetComponentInChildren<Camera>().enabled = false;
    }
}