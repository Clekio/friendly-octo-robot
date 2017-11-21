using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrouchCheck : MonoBehaviour
{
    public bool canStandUp = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canStandUp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canStandUp = true;
        }
    }
}