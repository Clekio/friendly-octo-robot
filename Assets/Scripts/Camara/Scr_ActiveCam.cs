using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ActiveCam : MonoBehaviour {

    public GameObject camera1;

    public GameObject camera2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
    }

}
