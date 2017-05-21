using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerEscalera : MonoBehaviour
{
    [SerializeField]
    Animator escalera;

    public static bool escaleraDown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        escaleraDown = true;
    }

    void Update()
    {
        escalera.SetBool("escaleraDown", escaleraDown);
    }
}