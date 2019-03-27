using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Shorcut : MonoBehaviour
{
    public string scene = "Level3";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public void SceneSwitcher()
    {
        // Load Level 1
        Initiate.Fade(scene, loadToColor, speed);
        //SceneManager.LoadScene(0);
    }
}
