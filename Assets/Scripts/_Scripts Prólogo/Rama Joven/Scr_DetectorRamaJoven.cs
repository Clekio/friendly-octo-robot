using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DetectorRamaJoven : MonoBehaviour
{
    public bool jugadorEnRama;
    public bool troncoEnRama;
    public bool ladoBueno = false;

    public int estado;

    // 0 = Estado inicial
    // 1 = Jugador en rama
    // 2 = Tronco en rama
    // 3 = Lanzamiento

    private void Awake()
    {
        estado = 0;
    }

    private void Update()
    {
        ladoBueno = gameObject.GetComponentInChildren<Scr_LadoBuenoRamaJoven>().ladoBueno;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            jugadorEnRama = true;

            if (troncoEnRama == true)
            {
                estado = 2;
            }
            else
            {
                estado = 1;
            }
        }

        if (collision.gameObject.tag == "Pull&Push")
        {
            troncoEnRama = true;

            estado = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            jugadorEnRama = false;

            if (troncoEnRama == true)
            {
                estado = 2;
            }
            else
            {
                estado = 0;
            }
        }

        if (collision.gameObject.tag == "Pull&Push")
        {
            troncoEnRama = false;

            if (jugadorEnRama == true && ladoBueno == true)
            {
                estado = 3;
            }

            else
            {
                estado = 0;
            }
        }
    }
}