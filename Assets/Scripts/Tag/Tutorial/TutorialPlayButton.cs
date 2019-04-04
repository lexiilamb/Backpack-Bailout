using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialPlayButton : MonoBehaviour
{
    public string scene = "Level3";
    public Color loadToColor = Color.white;
    public float speed = 3.0f;

    public GameObject skip;

    private IEnumerator stopTime;

    // Start is called before the first frame update
    void Start()
    {
        // Freeze game
        stopTime = freezeTime();
        StartCoroutine(stopTime);
    }

    IEnumerator freezeTime()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

        public void SkipTutorial()
    {
        // Load level 3
        Time.timeScale = 1f;
        Initiate.Fade(scene, loadToColor, speed);
    }

    public void PlayTutorial()
    {
        // Destroy object
        Destroy(skip);
        // Unfreeze game
        Time.timeScale = 1f;
    }
}
