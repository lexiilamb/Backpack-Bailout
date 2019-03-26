using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public string scene = "MainMenu";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public Canvas winCanvas;
    public Canvas loseCanvas;
    public Text setTime;
    public Text countText;

    private Rigidbody player;
    public int count;

    // NPC interaction flags
    public bool claireAskedForHelp = false;
    public bool wonPong = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();

        // Win/lose states disabled
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {

        //isGameOver();
    }

    //private void isGameOver()
    //{
    //    // Lose game
    //    if (currentTime > 10)
    //    {
    //        // Freeze game
    //        Time.timeScale = 0;
    //        loseCanvas.gameObject.SetActive(true);
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            SceneManager.LoadScene(3);
    //            //Initiate.Fade(scene, loadToColor, speed);
    //        }
    //    }
    //}

    public void gameWon()
    {
        // Play win sound/audio????
        // Freeze game and display win canvas
        Time.timeScale = 0f;
        winCanvas.gameObject.SetActive(true);
    }

    // Must use "Collider" as variable for trigger functions
    void OnTriggerEnter(Collider collision)
    {
 
    }

    public void SetCountText()
    {
        count = count + 1;
        countText.text = "Karma: " + count.ToString();
    }
}
