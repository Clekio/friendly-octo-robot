using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DestruirRama : MonoBehaviour
{
    [SerializeField]
    int timeToBreak;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("DestroyObject", timeToBreak);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}