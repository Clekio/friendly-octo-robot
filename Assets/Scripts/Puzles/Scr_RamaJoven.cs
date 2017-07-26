using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaJoven : MonoBehaviour
{
    [SerializeField]
    int impulso;

    [SerializeField]
    GameObject player;

    bool jugador;
    bool caja;
    bool ladoBueno;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Pull&Push")
        {
            caja = true;
        }

        if (collision.gameObject.tag == "Player")
        {
            jugador = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            caja = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            jugador = false;
        }

        if (jugador == true && ladoBueno == true)
        {
            Rigidbody2D rb2d = player.gameObject.GetComponent<Rigidbody2D>();

            if (!rb2d.isKinematic)
                rb2d.velocity = new Vector2(rb2d.velocity.x, impulso);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ladoBueno = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ladoBueno = false;
        }
    }
}