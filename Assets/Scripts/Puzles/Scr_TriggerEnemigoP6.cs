using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerEnemigoP6 : MonoBehaviour
{
    bool perseguir = false;

    public Rigidbody2D enemigo;
    public Collider2D other;

    public Vector2 force;
    public ForceMode2D mode;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("que voy");
        perseguir = true;
    }

    void Start ()
    {
        force = new Vector2(-5, 0);
	}
	
	void Update ()
    {
		if (perseguir == true)
        {
            enemigo.AddForce(force, mode = ForceMode2D.Force);
        }
	}
}
