using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ObjetoMuerte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerLive>().killAndRespawn();
    }
}