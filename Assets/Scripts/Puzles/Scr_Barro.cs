using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Barro : MonoBehaviour
{
    public Collider2D player;
    bool enBarro = false;
    public float reduccionVelocidad;

    void OnTriggerEnter2D(Collider2D player)
    {
        Debug.Log("pene");
        enBarro = true;
    }

    void Start()
    {
        reduccionVelocidad = .01f;
    }

    void Update()
    {
        if (enBarro == true)
        {
            Scr_PlayerVictor.Velocity.x -= reduccionVelocidad;
        }
    }
}