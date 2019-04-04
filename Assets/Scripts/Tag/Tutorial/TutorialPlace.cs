using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialPlace : MonoBehaviour
{
    public float currentTime = 0.0f;
    public Text setTime;

    // audio source
    public AudioSource _AudioSource;

    // Storage objects
    public GameObject laptopToPlace;
    private IEnumerator placedObject;

    //Inventory objects
    public GameObject laptopInventory;

    public GameObject xOutLaptopsUI;

    // Indecies 
    public int laptopCounter;

    // Collected objects
    public int numOfLaptops = 0;

    // Start is called before the first frame update
    void Start()
    {
        laptopCounter = 0;
        laptopToPlace.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        setTime.text = "Time: " + currentTime.ToString("F2") + " seconds";
    }

    IEnumerator placeThisObject()
    {
        // Activate enable object in storage shelf
        laptopToPlace.gameObject.SetActive(true);
        // Disable inventory icon on UI
        laptopInventory.gameObject.SetActive(false);
        xOutLaptopsUI.gameObject.SetActive(false);
        laptopCounter++;

        GameObject.FindWithTag("AJ").GetComponent<TutorialAJ>().collectionDialogueFlag = false;
        GameObject.FindWithTag("AJ").GetComponent<TutorialAJ>().finalDialogueFlag = true;

        yield return new WaitForSeconds(0.2f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LaptopShelf")
        {
            // Add item to shelf if player has some
            if (numOfLaptops > 0)
            {
                numOfLaptops--;
                _AudioSource.Play();
                placedObject = placeThisObject();
                StartCoroutine(placedObject);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {

    }
}
