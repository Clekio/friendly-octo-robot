using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_PlayerDeath : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    Vector3 lastCheckpoint;

    private void Start()
    {
        DontDestroyOnLoad(player);
    }

    private void Update()
    {
        lastCheckpoint = Scr_PlayerCheckpoint.lastCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.transform.position = lastCheckpoint;

            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Playtest", LoadSceneMode.Single);
    }
}