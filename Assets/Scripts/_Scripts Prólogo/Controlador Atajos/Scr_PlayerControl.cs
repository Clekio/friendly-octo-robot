﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_PlayerControl : MonoBehaviour
{
    public GameObject Aura;
    public GameObject evento1; // Evento caida tronco
    public GameObject evento2; // Evento escudo
    public GameObject evento3; // Evento corrupción

    private void Start()
    {
        /*Aura = GameObject.Find("Aura");

        evento1 = GameObject.Find("TroncoZona1");
        evento2 = GameObject.Find("Escudo");
        evento3 = GameObject.Find("Corrupción");*/
    }

    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameController.instance.GameOver(Aura);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            //LoadVictorScene();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadEscenaMagias();
        }
    }
    /*
    void Death()
    {
        Scr_GameControl.control.Save();
        Scr_GameControl.control.Load();

        if (Scr_GameControl.control.evento1 == true)
        {
            DontDestroyOnLoad(evento1);
        }

        if (Scr_GameControl.control.evento2 == true)
        {
            DontDestroyOnLoad(evento2);
        }

        if (Scr_GameControl.control.evento3 == true)
        {
            DontDestroyOnLoad(evento3);
        }

        SceneManager.LoadScene("Scn_Prólogo", LoadSceneMode.Single);
    }*/

    void LoadVictorScene()
    {
        SceneManager.LoadScene("Scn_PruebaVictor", LoadSceneMode.Single);
    }

    void LoadEscenaMagias()
    {
        SceneManager.LoadScene("Level_Agua_02", LoadSceneMode.Single);
    }
}