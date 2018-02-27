﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scr_ControladorMenuPpal : MonoBehaviour
{
    Animator logo;
    Animator textoStart;
    Animator jugar;
    Animator controles;

    bool intro = false;
    int button = 0;

    GameObject jugarButton;
    GameObject controlesButton;

    private void Awake()
    {
        logo = GameObject.Find("Logo").GetComponent<Animator>();
        textoStart = GameObject.Find("TextoStart").GetComponent<Animator>();
        jugar = GameObject.Find("Jugar").GetComponent<Animator>();
        controles = GameObject.Find("Controles").GetComponent<Animator>();

        jugarButton = GameObject.Find("Button 1");
        controlesButton = GameObject.Find("Button 2");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            logo.SetBool("On", true);
            textoStart.SetBool("On", true);
            jugar.SetBool("On", true);
            controles.SetBool("On", true);

            Invoke("ActivateButtons", 2);
        }
    }

    void ActivateButtons()
    {
        jugarButton.GetComponent<Button>().interactable = true;
        controlesButton.GetComponent<Button>().interactable = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}