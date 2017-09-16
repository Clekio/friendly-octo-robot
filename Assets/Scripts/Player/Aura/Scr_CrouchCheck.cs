using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrouchCheck : MonoBehaviour
{
    public static bool canStandUp = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "suelo")
        {
            canStandUp = false;
            //Debug.Log("YA NO PUEDES LEVANTARTE");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "suelo")
        {
            canStandUp = true;
            //Debug.Log("PUEDES LEVANTARTE");
        }
    }
}