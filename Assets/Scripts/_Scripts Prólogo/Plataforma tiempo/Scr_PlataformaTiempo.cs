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

    public Animator anim;
    public Collider2D coll;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && doBreak == true)
        {
            Invoke("DisablePlatform", timeToBreak);
        }
    }

    void DisablePlatform()
    {
        coll.enabled = false;
        anim.SetBool("Activo", false);
        Invoke("EnablePlatform", timeToSpawn);
    }

    void EnablePlatform()
    {
        coll.enabled = true;
        anim.SetBool("Activo", true);
    }
}