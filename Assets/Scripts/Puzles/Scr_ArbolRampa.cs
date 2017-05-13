using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ArbolRampa : MonoBehaviour
{
    public Rigidbody2D rb;

    public Vector2 force;
    public Vector2 position;
    public ForceMode2D mode;

    bool purificacion;
	
	void Update () {

        purificacion = Scr_TriggerArbolRampa.purificacionPosible;

        if (purificacion == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                force = new Vector3(-12, -12);
                rb.AddForceAtPosition(force, position, mode = ForceMode2D.Force);
            }
        }
    }
}