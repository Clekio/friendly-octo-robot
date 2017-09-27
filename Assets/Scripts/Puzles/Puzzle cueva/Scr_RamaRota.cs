using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaRota : MonoBehaviour
{
    [SerializeField]
    Animator ramaRota;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ramaRota.SetBool("Break", true);
        }
    }
}