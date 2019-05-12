using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject goResumeButton;
    public GameObject goBackButton;
    public GameObject goOptionButton;
    private Button bBackButton;
    private Button bResumeButton;
    private Button bOptionButton;

    // Update is called once per frame
    void Update()
    {
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        est-ce nécessaire?
         */

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Option()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);

        bBackButton = goBackButton.GetComponent<Button>();
        bBackButton.Select();
        bBackButton.OnSelect(null);
    }

    public void BackButton()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        bOptionButton = goOptionButton.GetComponent<Button>();
        bOptionButton.Select();
        bOptionButton.OnSelect(null);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        bResumeButton = goResumeButton.GetComponent<Button>();
        bResumeButton.Select();
        bResumeButton.OnSelect(null);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // choisir la scène du menu!
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game!"); // uniquement pour le dev
        Application.Quit();
    }
}
