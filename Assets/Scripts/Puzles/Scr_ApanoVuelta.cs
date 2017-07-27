using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ApanoVuelta : MonoBehaviour
{
    [SerializeField]
    GameObject mover1;

    [SerializeField]
    GameObject mover2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mover1.SetActive(true);
        mover2.SetActive(true);
    }
}