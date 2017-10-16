using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TroncoZona1Prologo : MonoBehaviour
{
    GameObject tronco;

    Animator anim;

    private void Start()
    {
        tronco = GameObject.Find("TroncoZona1");

        anim = gameObject.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Activate", true);
    }
}