using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCheckpoint : MonoBehaviour
{
    public static Vector3 lastCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            lastCheckpoint = gameObject.transform.position;

            Debug.Log("CP Activado");
        }
    }
}