using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaJoven : MonoBehaviour
{
    [SerializeField]
    int impulso;

    [SerializeField]
    GameObject player;

    bool jugadorEnRama;
    bool cajaEnRama;
    bool ladoBueno = false;

    private void Update()
    {
        ladoBueno = Scr_LadoBuenoRamaJoven.ladoBueno;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            cajaEnRama = true;
        }

        if (collision.gameObject.tag == "Player")
        {
            jugadorEnRama = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            cajaEnRama = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            jugadorEnRama = false;
        }

        if (jugadorEnRama == true && ladoBueno == true)
        {
            Rigidbody2D rb2d = player.gameObject.GetComponent<Rigidbody2D>();

            if (!rb2d.isKinematic)
                rb2d.velocity = new Vector2(rb2d.velocity.x, impulso);
        }
    }
}