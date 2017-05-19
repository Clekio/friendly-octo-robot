using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MenhirPlatsFlotantes : MonoBehaviour
{
    public static bool activarPlataformas = false;
    bool menhirActivado = false;
    public Collider2D other;

    void OnTriggerEnter2D(Collider2D other)
    {
        menhirActivado = true;
    }

    void Update()
    {

		if (menhirActivado == true) {
        if (Input.GetMouseButtonDown(1))
        {
            activarPlataformas = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -5);
        }
		}
    }
}