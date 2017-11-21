using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CorrupcionVuelta : MonoBehaviour
{
    public bool evento3;

    public bool vueltaActivada;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            evento3 = true;
            vueltaActivada = true;

            gameObject.GetComponent<MeshRenderer>().enabled = true;

            GameObject.Find("Suelo 1 (25) - Se destruye a la vuelta").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Suelo 1 (25) - Se destruye a la vuelta").GetComponent<BoxCollider2D>().enabled = false;

            GameObject.Find("Suelo 1 (50) - Vuelta").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Suelo 1 (50) - Vuelta").GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}