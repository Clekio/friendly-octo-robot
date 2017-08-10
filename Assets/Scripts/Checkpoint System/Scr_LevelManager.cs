using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    [SerializeField]
    GameObject player;

    private void Start()
    {
        //player = FindObjectOfType<Scr_Player>();
    }
    public void RespawnPlayer()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}