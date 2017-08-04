using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerTrampa : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D balancin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            balancin.freezeRotation = true;
        }
    }
}