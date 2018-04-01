using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerCheckpoint : MonoBehaviour
{
    public int order;
    public Transform spawnPos;
    [HideInInspector]
    public Scr_PlayerCheckpoint thisCP;
    private void Awake()
    {
        thisCP = this;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameController.instance.AddCheckPoint(this);
        }
    }
    /*
    public static Vector3 checkpoint;

    public static string checkpointName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkpoint = gameObject.transform.position;

            checkpointName = gameObject.name;
        }
    }*/
}