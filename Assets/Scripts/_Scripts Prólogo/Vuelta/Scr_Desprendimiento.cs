using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Desprendimiento : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Suelo 1 (3) - Vuelta").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Suelo 1 (3) - Vuelta").GetComponent<BoxCollider2D>().enabled = true;

            GameObject.Find("Suelo 1 (6) - Vuelta").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Suelo 1 (6) - Vuelta").GetComponent<BoxCollider2D>().enabled = true;

            GameObject.Find("Suelo 1 (7) - Vuelta").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Suelo 1 (7) - Vuelta").GetComponent<BoxCollider2D>().enabled = true;

            GameObject.Find("Suelo 1 (18) - Vuelta").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Suelo 1 (18) - Vuelta").GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}