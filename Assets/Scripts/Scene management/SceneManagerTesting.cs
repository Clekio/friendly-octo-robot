using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTesting : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(0);
        }
    }

}
