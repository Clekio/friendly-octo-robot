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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camaraMovil.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        camaraMovil.SetActive(true);
    }
}