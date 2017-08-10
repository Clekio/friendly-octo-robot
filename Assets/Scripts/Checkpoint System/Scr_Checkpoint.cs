using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Checkpoint : MonoBehaviour
{
    public Scr_LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<Scr_LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Aura")
        {
            levelManager.currentCheckpoint = gameObject;

            Debug.Log("Last checkpoint: " + levelManager.currentCheckpoint.name);
        }
    }
}