using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_EmpujarTirarSetActive : MonoBehaviour
{
    public  Vector3 inicialPos;

    void Start()
    {
        inicialPos = transform.position;
    }

    private void Update()
    {
        Vector3 inCameraPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 inCameraInitialPos = Camera.main.WorldToViewportPoint(inicialPos);

        //Debug.Log("" + inCameraPos.x + " " + inCameraPos.y + " " + inCameraInitialPos.x + " " + inCameraInitialPos.y);
        if (((inCameraPos.x < 0 || inCameraPos.x > 1)
            || (inCameraPos.y < 0 || inCameraPos.y > 1))
            && ((inCameraInitialPos.x < 0 || inCameraInitialPos.x > 1)
            || (inCameraInitialPos.y < 0 || inCameraInitialPos.y > 1)))
            transform.position = inicialPos;
    }
    
}