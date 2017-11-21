using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestruirTronco : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(GameObject.Find("TroncoInicioZona2"));

            Destroy(GameObject.Find("Suelo 1 (18) - Vuelta"));
        }
    }
}