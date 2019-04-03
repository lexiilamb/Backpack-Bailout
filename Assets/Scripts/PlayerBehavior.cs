using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public AudioSource _audioSource;
    public string scene = "MainMenu";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public Canvas winCanvas;
    public Text countText;

    private Rigidbody player;
    public int count;

    // NPC interaction flags
    public bool claireAskedForHelp = false;
    public bool wonPong = false;
    public bool wonGame = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();

        // Win/lose states disabled
        winCanvas.gameObject.SetActive(false);
    }

    void Update()
    {

        if(wonGame)
        {
            wonGame = false;
            GameWon();
        }
    }


    public void GameWon()
    {
        // Freeze game and display win canvas
        //Time.timeScale = 0f;
        winCanvas.gameObject.SetActive(true);
        _audioSource.Play();
    }

    public void SetCountText()
    {
        count = count + 1;
        countText.text = "Karma: " + count.ToString();
    }
}
