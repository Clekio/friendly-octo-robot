using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_BotonDelPanico : MonoBehaviour
{
    GameObject Aura;

    Vector3 checkpoint;
    string checkpointName;

    private void Start()
    {
        Aura = GameObject.Find("Aura");

        DontDestroyOnLoad(Aura);
    }

    private void Update()
    {
        checkpoint = Scr_PlayerCheckpoint.checkpoint;
        checkpointName = Scr_PlayerCheckpoint.checkpointName;

        if (Input.GetKeyDown(KeyCode.K))
        {
            Death();
        }
    }

    void Death()
    {
        SceneManager.LoadScene("Playtest", LoadSceneMode.Single);

        Aura.transform.position = checkpoint;
    }
}