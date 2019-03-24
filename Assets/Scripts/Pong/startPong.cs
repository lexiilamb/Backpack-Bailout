using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startPong : MonoBehaviour
{
    Transform target;

    private IEnumerator coroutine;
    public Image image;
    Color color;
    public GameObject myObject;
    private Camera setMainCamera;
    public GameObject gameCamera;
    public bool startGamePong = false;
    private bool pongStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        myObject.SetActive(false);
        gameCamera.SetActive(false);
        setMainCamera = GameObject.FindWithTag("Player").GetComponent<CharacterControl>()._camera;
    }

    // Update is called once per frame
    void Update()
    {
        setMainCamera = GameObject.FindWithTag("Player").GetComponent<CharacterControl>()._camera;

        if(startGamePong)
        {
            coroutine = FadeOut();
            StartCoroutine(coroutine);
            startGamePong = false;
        }

        if (pongStarted)
        {
            myObject.SetActive(true);
            gameCamera.SetActive(true);
            pongStarted = false;
        }
    }

    IEnumerator FadeOut()
    {
        // alpha cannot be 0 to use CrossFadeAlpha
        color.a = 0.01f;
        image.color = color;
        // Fade out to black and face board
        image.CrossFadeAlpha(255f, 3.0f, false);
        yield return new WaitForSeconds(3);
        var pos = transform.position;
        pos.x = 4.2f;
        pos.y = 0.942f;
        pos.z = 145.0f;
        target.transform.position = pos;
        // Fade back into game
        setMainCamera.transform.eulerAngles = new Vector3(0, 180, 0);
        image.CrossFadeAlpha(0f, 3.0f, false);
        yield return new WaitForSeconds(3);
        pongStarted = true;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Fire2"))
            {
                // Resume player movement 
                GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myObject.SetActive(false);
            gameCamera.SetActive(false);
        }
    }
}
