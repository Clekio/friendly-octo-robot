using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CaidaRamas : MonoBehaviour
{
    [SerializeField]
    int timeToBreak;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            Invoke("DisablePlatform", timeToBreak);
        }
    }

    void DisablePlatform()
    {
        Destroy(gameObject);
    }
}