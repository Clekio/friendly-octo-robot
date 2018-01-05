using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DisablePlayerInput : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Aura").GetComponent<Aura>().canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Aura").GetComponent<Aura>().canMove = true;
        }
    }
}