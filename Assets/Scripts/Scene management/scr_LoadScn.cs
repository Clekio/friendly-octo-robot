using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_LoadScn : MonoBehaviour {

    public string scene2Load;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(scene2Load, LoadSceneMode.Additive);
        }
    }
}
