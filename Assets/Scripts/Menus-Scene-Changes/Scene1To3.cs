using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene1To3 : MonoBehaviour
{
    public string scene = "Tutorial";
    public Color loadToColor = Color.white;
    public float speed = 1.0f;
    public GameObject youShallNotPass;

    void Start()
    {
        youShallNotPass.SetActive(false);
    }

    // Must use "Collider" as variable for trigger functions
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerBehavior>().claireAskedForHelp)
            {
                // Load level 2
                Initiate.Fade(scene, loadToColor, speed);
            }
            else
            {
                // Activate button
                youShallNotPass.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Deactivate button
            youShallNotPass.SetActive(false);
        }
    }
}
