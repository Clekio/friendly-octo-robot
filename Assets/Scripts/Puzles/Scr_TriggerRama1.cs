using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRama1 : MonoBehaviour
{
    [SerializeField]
    Animator rama1;

    public static bool rama1Down = false;

    bool activado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activado == true)
        {
            rama1Down = true;
        }
    }

    void Update()
    {
        rama1.SetBool("rama1Down", rama1Down);
        activado = Scr_TriggerRaicesVuelta1.trigger1Activado;
    }
}