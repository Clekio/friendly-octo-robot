using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_triggerCorrupcion : MonoBehaviour
{
    [SerializeField]
    GameObject corrupcion;

    private void Update()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            corrupcion.SetActive(true);
        }
    }
}