using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrouchCheck : MonoBehaviour
{
    public static bool canStandUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canStandUp = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canStandUp = false;
    }
}