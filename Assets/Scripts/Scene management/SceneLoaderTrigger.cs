using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NextSceneDir { Up, Right, Down, Left}
public class SceneLoaderTrigger : MonoBehaviour
{
    public NextSceneDir sceneDir;

    public string NextScene;
    public string PreviousScene;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        bool next = false;
        //Con numeros
        //Debug.Log(next = collision.transform.position.y > transform.position.y);

        switch (sceneDir)
        {
            case NextSceneDir.Up:
                next = collision.transform.position.y > transform.position.y;
                break;

            case NextSceneDir.Right:
                next = collision.transform.position.x > transform.position.x;
                break;

            case NextSceneDir.Down:
                next = collision.transform.position.y < transform.position.y;
                break;

            case NextSceneDir.Left:
                next = collision.transform.position.x < transform.position.x;
                break;
        }

        if (next)
        {
            if (!string.IsNullOrEmpty(NextScene) && !SceneManager.GetSceneByName(NextScene).isLoaded)
                SceneManager.LoadSceneAsync(NextScene, LoadSceneMode.Additive);//StartCoroutine(LoadYourAsyncScene(NextScene));

            if (!string.IsNullOrEmpty(PreviousScene) && SceneManager.GetSceneByName(PreviousScene).isLoaded)
                SceneManager.UnloadSceneAsync(PreviousScene);//StartCoroutine(UnloadYourAsyncScene(PreviousScene));
        }
        else
        {
            if (!string.IsNullOrEmpty(PreviousScene) && !SceneManager.GetSceneByName(PreviousScene).isLoaded)
                SceneManager.LoadSceneAsync(PreviousScene, LoadSceneMode.Additive);//StartCoroutine(LoadYourAsyncScene(PreviousScene));

            if (!string.IsNullOrEmpty(NextScene) && SceneManager.GetSceneByName(NextScene).isLoaded)
                SceneManager.UnloadSceneAsync(NextScene);//StartCoroutine(UnloadYourAsyncScene(NextScene));
        }
    }

    //IEnumerator LoadYourAsyncScene(string sceneName)
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //        Debug.Log(sceneName + " Cargada");
    //    }
    //}

    IEnumerator UnloadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone)
        {
            yield return null;
            Debug.Log(sceneName + " Descargada");
        }
    }
}
