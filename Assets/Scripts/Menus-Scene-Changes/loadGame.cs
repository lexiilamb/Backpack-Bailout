using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    public string scene = "Level1";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    // 0 = easy, 1 = medium, 2 = hard
    public int gameDifficulty = 0;

    public void SceneSwitcher ()
    {
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void EasyGame()
    {
        gameDifficulty = 0;
        Debug.Log("Game mode is: " + gameDifficulty);
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void MediumGame()
    {
        gameDifficulty = 1;
        Debug.Log("Game mode is: " + gameDifficulty);
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void HardGame()
    {
        gameDifficulty = 2;
        Debug.Log("Game mode is: " + gameDifficulty);
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void QuitGame()
    {
        // Quit Game
        Application.Quit();
    }
}
