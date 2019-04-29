using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialResume : MonoBehaviour
{
    public string scene = "MainMenu";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject volumeMenu;

    public bool skipMenuDestroyed = false;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        volumeMenu.SetActive(false);
        if (skipMenuDestroyed)
        {
            // Unfreeze game if skip menu has been deleted
            Time.timeScale = 1f;
        }
        GameIsPaused = false;
    }

    public void MainMenu()
    {
        // Load main menu
        Time.timeScale = 1f;
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void Quit()
    {
        // Quit Game
        Application.Quit();
    }
}
