using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Shortcut : MonoBehaviour
{
    public string scene = "Level3";
    public Color loadToColor = Color.yellow;
    public float speed = 1.0f;

    public void SceneSwitcher()
    {
        // Load Level 3
        Initiate.Fade(scene, loadToColor, speed);
    }
}
