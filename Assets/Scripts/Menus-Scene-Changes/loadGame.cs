using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    public string scene = "Level1";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public void EasyGame()
    {
        GameObject.Find("Difficulty").GetComponent<GameDifficulty>().setModeEasy();
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void MediumGame()
    {
        GameObject.Find("Difficulty").GetComponent<GameDifficulty>().setModeMedium();
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void HardGame()
    {
        GameObject.Find("Difficulty").GetComponent<GameDifficulty>().setModeHard();
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void QuitGame()
    {
        // Quit Game
        Application.Quit();
    }
}
