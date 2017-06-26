using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerTroncoRampa : MonoBehaviour
{
    [SerializeField]
    Animator arbolRampa;

    public static bool arbolDown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        arbolDown = true;
    }

    void Update()
    {
        arbolRampa.SetBool("arbolDown", arbolDown);
    }
}