using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ArbolRampa : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    bool purificado = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        purificado = Scr_TriggerArbolRampa.purificacionPosible;

        if (purificado)// == true)
        {
            anim.SetBool("Purificado", purificado);
        }
    }
}