using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objRamaDebil : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pull&Push"))
            Destroy(gameObject);
    }
}
