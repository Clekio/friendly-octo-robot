using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerTroncoRampa : MonoBehaviour
{
    [SerializeField]
    Animator arbolRampa;

    public static bool arbolDown = false;

    bool activado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activado == true)
        {
            arbolDown = true;
        }
    }

    void Update()
    {
        arbolRampa.SetBool("arbolDown", arbolDown);
        activado = Scr_TriggerRaicesVuelta1.trigger1Activado;
    }
}