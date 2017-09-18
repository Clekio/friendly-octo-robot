using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boton2 : MonoBehaviour 
{
    [SerializeField]
    Animator boton;

    [SerializeField]
    Animator sueloMovil;

    int estado;

    bool cajaIn = false;

    private void Start()
    {
        estado = 0;
    }

    private void Update()
    {
        if (estado == 0)
            boton.SetFloat("Move", 0);
        else if (estado == 1)
        {
            boton.SetFloat("Move", 2);
            sueloMovil.SetBool("Move", false);
        }

        else if (estado == -1)
        {
            boton.SetFloat("Move", -2);
            sueloMovil.SetBool("Move", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push" || collision.gameObject.tag == "Player")
        {
            estado = -1;
            Debug.Log("ADSSDS");
        }

        if (collision.gameObject.tag == "Pull&Push")
        {
            cajaIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push" || collision.gameObject.tag == "Player")
        {
            if (cajaIn == true)
                estado = -1;
            else
                estado = 1;
        }
    }
}
