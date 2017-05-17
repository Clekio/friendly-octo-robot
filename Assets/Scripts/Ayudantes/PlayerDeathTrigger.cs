using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Dentro");
        if (other.CompareTag("Player"))
        {
            Debug.Log("DentroPlayer");
            StartCoroutine(other.gameObject.GetComponent<PlayerLive>().killAndRespawn());
        }
    }
}
