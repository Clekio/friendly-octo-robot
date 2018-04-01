using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerDeath : MonoBehaviour
{
    public static Scr_PlayerDeath instance;

    Vector3 checkpoint;
    string checkpointName;

    private void Start()
    {
        //Aura = GameObject.Find("Aura");//NO HAGAIS FIND, GameObject.find es lo peor de lo peor.
    }

    /*private void Update()
    {
        checkpoint = Scr_PlayerCheckpoint.checkpoint;
        checkpointName = Scr_PlayerCheckpoint.checkpointName;

        //Debug.Log("Last checkpoint (" + checkpointName + ") position: " + checkpoint); // NO BORRAR
    }*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameController.instance.GameOver(col.gameObject);
        }
    }

    /*public void Death()
    {
        SceneManager.LoadScene("Playtest", LoadSceneMode.Single);

        Aura.transform.position = checkpoint;
    }*/

}