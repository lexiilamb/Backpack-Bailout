using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public AudioSource _AudioSource;
    public AudioClip _AudioClip1;
    public string scene = "MainMenu";
    public Color loadToColor = Color.black;
    public float speed = 1.0f;

    public Canvas winCanvas;
    public Text countText;

    private Rigidbody player;
    public int count;

    // NPC interaction flags
    public bool claireAskedForHelp = false;
    public bool wonGame = false;
    private bool displayedWinCanvas = false;
    public bool checkWonGame = true;

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
            Debug.Log("Game won");

            wonGame = false;
            GameWon();
        }
    }

    public void GameWon()
    {
        if (!displayedWinCanvas)
        {
            displayedWinCanvas = true;
            // Display win canvas
            _AudioSource.clip = _AudioClip1;
            _AudioSource.Play();
            winCanvas.gameObject.SetActive(true);
        }
    }

    public void SetCountText()
    {
        count = count + 1;
        countText.text = "Karma: " + count.ToString();
    }
}
