using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BoxTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool inside;
    [HideInInspector]
    public bool insideStart;

    private void Awake()
    {
        if (insideStart)
        {
            inside = true;
        }

        else
        {
            inside = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pull&Push")
        {
            inside = false;
        }
    }
}