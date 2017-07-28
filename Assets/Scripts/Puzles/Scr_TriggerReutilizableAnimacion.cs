using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerReutilizable : MonoBehaviour
{
    [SerializeField]
    Animator animacion;

    bool animar = false;

    bool activado = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        animar = true;
    }

    void Update()
    {
        animacion.SetBool("animate", animar);
        activado = Scr_TriggerRaicesVuelta1.trigger1Activado;
    }
}