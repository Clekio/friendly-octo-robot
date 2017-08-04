using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ComprobacionPlatsTiempo : MonoBehaviour
{
    [SerializeField]
    GameObject platTrampa1;

    [SerializeField]
    GameObject platTrampa2;

    [SerializeField]
    Transform position1;

    [SerializeField]
    Transform position2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(platTrampa1 = null)
            {
                Instantiate(platTrampa1, position1);
                Debug.Log("ASDFGHJ");
            }

            if (platTrampa2 = null)
            {
                Instantiate(platTrampa2, position2);
            }
        }
    }
}