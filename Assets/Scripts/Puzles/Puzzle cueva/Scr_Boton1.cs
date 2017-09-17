using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Boton1 : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    int estado;

    private void Start()
    {
        estado = 0;
    }

    private void Update()
    {
        if (estado == 0)
            anim.SetFloat("Move", 0);
        else if (estado == 1)
            anim.SetFloat("Move", 2);
        else if (estado == -1)
            anim.SetFloat("Move", -2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        estado = -1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        estado = 1;
    }
}