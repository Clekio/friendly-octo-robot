using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_PlayerControl : MonoBehaviour
{
    GameObject Aura;
    GameObject evento1; // Evento caida tronco

    Vector3 checkpoint;
    string checkpointName;

    private void Start()
    {
        Aura = GameObject.Find("Aura");
        evento1 = GameObject.Find("TroncoZona1");

        DontDestroyOnLoad(Aura);
    }

    void Update ()
    {
        checkpoint = Scr_PlayerCheckpoint.checkpoint;
        checkpointName = Scr_PlayerCheckpoint.checkpointName;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            LoadVictorScene();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadEscenaMagias();
        }
    }

    void Death()
    {
        Scr_GameControl.control.Save();
        Scr_GameControl.control.Load();

        if (Scr_GameControl.control.evento1 == true)
        {
            DontDestroyOnLoad(evento1);
        }

        SceneManager.LoadScene("Scn_Prólogo", LoadSceneMode.Single);

        Aura.transform.position = checkpoint;
    }

    void LoadVictorScene()
    {
        SceneManager.LoadScene("Scn_PruebaVictor", LoadSceneMode.Single);
    }

    void LoadEscenaMagias()
    {
        SceneManager.LoadScene("Level_Agua_02", LoadSceneMode.Single);
    }
}