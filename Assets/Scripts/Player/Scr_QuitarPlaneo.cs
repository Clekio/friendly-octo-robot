using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_QuitarPlaneo : MonoBehaviour
{
    public static bool planear = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            planear = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            planear = true;
        }
    }
}