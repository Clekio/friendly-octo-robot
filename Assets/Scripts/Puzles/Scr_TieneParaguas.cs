using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TieneParaguas : MonoBehaviour
{
    public static bool paraguas = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        paraguas = true;
    }
}