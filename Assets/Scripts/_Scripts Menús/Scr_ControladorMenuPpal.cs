using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ControladorMenuPpal : MonoBehaviour
{
    Animator logo;
    Animator textoStart;
    Animator jugar;
    Animator controles;

    bool intro = false;

    private void Awake()
    {
        logo = GameObject.Find("Logo").GetComponent<Animator>();
        textoStart = GameObject.Find("TextoStart").GetComponent<Animator>();
        jugar = GameObject.Find("Jugar").GetComponent<Animator>();
        controles = GameObject.Find("Controles").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            logo.SetBool("On", true);
            textoStart.SetBool("On", true);
            jugar.SetBool("On", true);
            controles.SetBool("On", true);
        }
    }
}