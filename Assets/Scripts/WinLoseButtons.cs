using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseButtons : MonoBehaviour
{
    public string scene = "MainMenu";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public static bool GameIsPaused = false;
    public GameObject canvasToTurnOff;

    public void MainMenu()
    {
        // Load main menu
        Time.timeScale = 1f;
        canvasToTurnOff.gameObject.SetActive(false);
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void Quit()
    {
        // Quit Game
        Debug.Log("Quit");
        Application.Quit();
    }
}
