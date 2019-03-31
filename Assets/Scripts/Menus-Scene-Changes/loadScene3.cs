using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene3 : MonoBehaviour
{
    public string scene = "Level3";
    public Color loadToColor = Color.yellow;
    public float speed = 1.0f;

    // Must use "Collider" as variable for trigger functions
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Load level 3
            Initiate.Fade(scene, loadToColor, speed);
        }

    }
}
