using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TroncoZona1Prologo : MonoBehaviour
{
    Animator anim;

    public bool evento1;

    private void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (evento1 == true)
        {
            anim.SetBool("Activate", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        evento1 = true;
    }
}