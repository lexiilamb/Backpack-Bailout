using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public string scene = "MainMenu";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        // Unfreeze game
        Time.timeScale = 1f;
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
        Debug.Log("Quit");
        Application.Quit();
    }
}
