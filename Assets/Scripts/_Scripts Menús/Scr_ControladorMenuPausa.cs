using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class Scr_ControladorMenuPausa : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    [SerializeField]
    public InputDevice input;

    public static bool GameIsPaused = false;

    private void Awake()
    {
        input = InputManager.ActiveDevice;
    }

    private void Update()
    {
        if (input.MenuWasPressed || Input.GetKeyDown(KeyCode.Escape)) //Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}