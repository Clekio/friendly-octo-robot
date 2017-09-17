using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlataformaTiempo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(DisablePlatform());
        }
    }

    IEnumerator DisablePlatform()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(EnablePlatform());
    }

    IEnumerator EnablePlatform()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}