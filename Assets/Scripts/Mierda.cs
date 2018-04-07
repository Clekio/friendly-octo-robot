using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mierda : MonoBehaviour
{
    public Vector3 puntero;

    public Vector3 A;

    public float Angulo1;
    public float Angulo2;
	
	// Update is called once per frame
	void Update ()
    {
        //puntero = Input.mousePosition;
        Angulo1 = Vector2.Angle(transform.position - A, transform.position - puntero);
        Angulo2 = Vector2.Angle(transform.position - puntero, transform.position - A);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(puntero, 0.25f);
        Gizmos.DrawLine(transform.position, puntero);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.25f);
        Gizmos.DrawSphere(A, 0.25f);
        Gizmos.DrawLine(transform.position, A);
    }
}
