using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_UnloadScene : MonoBehaviour {

    public string scnUnload;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.UnloadSceneAsync(scnUnload);
        }
    }

}
