using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LadoBuenoRamaJoven : MonoBehaviour
{
    public static bool ladoBueno = false;

    [SerializeField]
    Animator anim;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ladoBueno = true;

            anim.SetBool("Active", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ladoBueno = false;

            anim.SetBool("Active", false);
        }
    }
}