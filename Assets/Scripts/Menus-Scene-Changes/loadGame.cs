using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    public string scene = "Level1";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public void SceneSwitcher ()
    {
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void QuitGame()
    {
        // Quit Game
        Application.Quit();
    }
}
