using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_EmpujarTirarSetActive : MonoBehaviour
{
    public static Vector3 inicialPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inicialPos = gameObject.transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}