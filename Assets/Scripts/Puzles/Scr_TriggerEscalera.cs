using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerEscalera : MonoBehaviour
{
    [SerializeField]
    Animator escalera;

    public static bool escaleraDown = false;

    bool activado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activado == true)
        {
            escaleraDown = true;
        }
    }

    void Update()
    {
        escalera.SetBool("escaleraDown", escaleraDown);
        activado = Scr_TriggerRaicesVuelta1.trigger1Activado;
    }
}