using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CorrupcionVuelta : MonoBehaviour
{
    public bool evento3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            evento3 = true;

            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}