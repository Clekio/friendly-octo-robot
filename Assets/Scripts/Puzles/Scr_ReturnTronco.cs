using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ReturnTronco : MonoBehaviour
{
    GameObject troncoActivo;

    Vector3 inicialPos;

    private void Update()
    {
        inicialPos = Scr_EmpujarTirarSetActive.inicialPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            troncoActivo = collision.gameObject;

            troncoActivo.transform.position = inicialPos;
        }
    }
}