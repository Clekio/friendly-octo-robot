using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaJoven : MonoBehaviour
{
    [SerializeField]
    int impulso;

    Animator anim;

    GameObject player;

    bool jugadorEnRama;
    bool cajaEnRama;
    bool ladoBueno = false;

    private void Start()
    {
        player = GameObject.Find("Aura");

        anim = gameObject.GetComponentInParent<Animator>();
    }

    private void Update()
    {
        ladoBueno = Scr_LadoBuenoRamaJoven.ladoBueno;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            cajaEnRama = true;

            anim.SetInteger("Estado", 2);
        }

        if (collision.gameObject.tag == "Player")
        {
            jugadorEnRama = true;

            anim.SetInteger("Estado", 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            cajaEnRama = false;

            anim.SetInteger("Estado", 0);

            if (jugadorEnRama == true && ladoBueno == true)
            {
                Rigidbody2D rb2d = player.gameObject.GetComponent<Rigidbody2D>();

                if (!rb2d.isKinematic)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, impulso);

                    anim.SetInteger("Estado", 3);
                }
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            jugadorEnRama = false;

            if (cajaEnRama == false)
            {
                anim.SetInteger("Estado", 0);
            }
        }
    }
}