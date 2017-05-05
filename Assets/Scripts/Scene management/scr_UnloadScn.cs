using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_UnloadScn : MonoBehaviour {

    public string scene2Unload;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.UnloadSceneAsync(scene2Unload);
        }
    }

}
