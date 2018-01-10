using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlataformaTiempo : MonoBehaviour
{
    [SerializeField]
    bool doBreak;
    
    [SerializeField]
    float timeToBreak;

    [SerializeField]
    int timeToSpawn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && doBreak == true)
        {
            Invoke("DisablePlatform", timeToBreak);
        }
    }

    void DisablePlatform()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("EnablePlatform", timeToSpawn);
    }

    void EnablePlatform()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}