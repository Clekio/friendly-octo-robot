using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaVieja : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
}