using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_ControladorAtajosPrologo : MonoBehaviour
{
    GameObject Aura;
    GameObject troncoZona1;
    GameObject corrupcion;
    GameObject escudo;

    Vector3 checkpoint;
    string checkpointName;

    private void Start()
    {
        Aura = GameObject.Find("Aura");
        troncoZona1 = GameObject.Find("TroncoZona1");
        escudo = GameObject.Find("Escudo");
        corrupcion = GameObject.Find("Corrupción");

        DontDestroyOnLoad(Aura);
        DontDestroyOnLoad(troncoZona1);
        DontDestroyOnLoad(escudo);
        DontDestroyOnLoad(corrupcion);
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

        if (Input.GetKeyDown(KeyCode.V))
        {
            LoadEscenaMagias();
        }
    }

    void Death()
    {
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