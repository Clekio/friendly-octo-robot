using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlatCaida : MonoBehaviour
{
    [SerializeField]
    GameObject destruir;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(destruir, 1);
        }
    }
}