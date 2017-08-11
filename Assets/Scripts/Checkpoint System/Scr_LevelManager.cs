using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    [SerializeField]
    GameObject player;

    private void Start()
    {
        //player = FindObjectOfType<Scr_Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.transform.position = currentCheckpoint.transform.position;
        }
    }

    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}