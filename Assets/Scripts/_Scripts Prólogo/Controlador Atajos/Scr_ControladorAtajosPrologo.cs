using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_ControladorAtajosPrologo : MonoBehaviour
{
    GameObject Aura;
    GameObject troncoZona1;

    Vector3 checkpoint;
    string checkpointName;

    private void Start()
    {
        Aura = GameObject.Find("Aura");
        troncoZona1 = GameObject.Find("TroncoZona1");

        DontDestroyOnLoad(Aura);
        DontDestroyOnLoad(troncoZona1);
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
            LoadVictor();
        }
    }

    void LoadVictor()
    {
        SceneManager.LoadScene("Scn_PruebaVictor", LoadSceneMode.Single);
    }

    void Death()
    {
        SceneManager.LoadScene("Scn_Prólogo", LoadSceneMode.Single);

        Aura.transform.position = checkpoint;
    }
}