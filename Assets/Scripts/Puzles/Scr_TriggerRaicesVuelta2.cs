using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerRaicesVuelta2 : MonoBehaviour
{
    [SerializeField]
    Animator raicesVuelta;

    bool trigger2Activado = false;
    bool raicesDown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (trigger2Activado == true)
        {
            raicesDown = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -30);
        }
    }

    void Update()
    {
        trigger2Activado = Scr_TriggerRaicesVuelta1.trigger1Activado;
        raicesVuelta.SetBool("espinasDown", raicesDown);
    }
}