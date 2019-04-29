using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollect : MonoBehaviour
{
    public AudioSource _AudioSource;

    public GameObject prompt;
    public GameObject xOutLaptopsUI;

    // Prevent objects from being counted twice
    private bool canDestroyObject = true;

    private IEnumerator collectedObject;


    // Start is called before the first frame update
    void Start()
    {
        collectedObject = destroyObject();
        xOutLaptopsUI.gameObject.SetActive(false);
        prompt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator destroyObject()
    {
        FindObjectOfType<TutorialPlace>().numOfLaptops++;

        _AudioSource.Play();

        // Do collection aniamtion/effect
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        // Destroy effect
        Destroy(this.transform.GetChild(0).gameObject);
        // Destroy object
        Destroy(this.gameObject);
        xOutLaptopsUI.gameObject.SetActive(true);
    }

    public void startDestroyCoroutine()
    {
        canDestroyObject = false;
        collectedObject = destroyObject();
        StartCoroutine(collectedObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Collect object if talked to AJ
            if (GameObject.FindWithTag("AJ").GetComponent<TutorialAJ>().canCollectLaptop)
            {
                if(canDestroyObject)
                    startDestroyCoroutine();
            }
            else
            {
                // Prompt player to talk to AJ
                prompt.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Prompt player to talk to AJ
            prompt.gameObject.SetActive(false);
        }
    }
}
