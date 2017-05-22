using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRama2 : MonoBehaviour
{
    [SerializeField]
    Animator rama2;

    public static bool rama2Down = false;

    bool activado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activado == true)
        {
            rama2Down = true;
        }
    }

    void Update()
    {
        rama2.SetBool("rama2Down", rama2Down);
        activado = Scr_TriggerRaicesVuelta1.trigger1Activado;
    }
}