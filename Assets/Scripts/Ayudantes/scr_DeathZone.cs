using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DeathZone : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            scr_Respawn.KillPlayer();
    }
}
