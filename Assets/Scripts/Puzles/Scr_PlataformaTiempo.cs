using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlataformaTiempo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("DisablePlatform", 1);
        }
    }

    void DisablePlatform()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("EnablePlatform", 3);
    }

    void EnablePlatform()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}