﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ApanoVuelta : MonoBehaviour
{
    [SerializeField]
    GameObject mover1;

    [SerializeField]
    GameObject mover2;

    [SerializeField]
    GameObject vuelta1;

    [SerializeField]
    GameObject vuelta2;

    [SerializeField]
    GameObject balancin;

    [SerializeField]
    GameObject hueco;

    [SerializeField]
    GameObject triggerParaguas;

    [SerializeField]
    GameObject tronco;

    [SerializeField]
    GameObject suelo30;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mover1.SetActive(true);
        mover2.SetActive(true);

        vuelta1.SetActive(true);
        vuelta2.SetActive(true);

        balancin.SetActive(false);

        tronco.SetActive(false);
        hueco.SetActive(true);

        triggerParaguas.SetActive(true);

        suelo30.SetActive(true);
    }
}