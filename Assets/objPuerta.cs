using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objPuerta : MonoBehaviour {

    public List<GameObject> triangulos = new List<GameObject>();

    public SpriteRenderer sRender;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && areTriangles())
        {
            sRender.color = Colors.Magenta;
        }

    }

    private bool areTriangles()
    {
        bool b = true;

        foreach (GameObject g in triangulos)
        {
            if (g.activeSelf)
            {
                b = false;
            }
        }

        return b;
    }
}
