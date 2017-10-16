using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_ControladorAtajosPrologo : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            LoadVictor();
        }
    }

    void LoadVictor()
    {
        SceneManager.LoadScene("Scn_PruebaVictor", LoadSceneMode.Single);
    }
}