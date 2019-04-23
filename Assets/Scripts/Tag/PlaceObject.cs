using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    // Indecies for categories 
    public int tabletsIndex = 0;
    public int calculatorsIndex = 1;
    public int notebooksIndex = 2;
    public int laptopsIndex = 3;
    private int[] indexArray;

    // Shelves object tags
    private string[] shelfTagNames = new string[] { "TabletShelf", "CalculatorShelf", "NotebookShelf", "LaptopShelf" };
    public int[] categoryCounters = new int[] { 0, 0, 0, 0};

    public float currentTime = 0.0f;
    public Text setTime;

    private IEnumerator placedObject;

    // audio source
    public AudioSource _AudioSource;

    // Storage objects
    public GameObject tablets;
    public GameObject calculators;
    public GameObject notebooks;
    public GameObject laptops;

    //Inventory objects
    public GameObject tabInventory;
    public GameObject calcInventory;
    public GameObject notebookInventory;
    public GameObject laptopInventory;
    public GameObject[] inventoryObjectsArray;

    private GameObject xTabletsUI;
    private GameObject xCalculatorsUI;
    private GameObject xNotebooksUI;
    private GameObject xLaptopsUI;
    private GameObject[] xObjectsArrayUI;

    // Collected objects
    public int[] amountCollected = new int[] { 0, 0, 0, 0 };

    public int numToDeactivate;

    // Check once for shelves being full for AJ script
    private bool checkedT = false;
    private bool checkedC = false;
    private bool checkedN = false;
    private bool checkedL = false;

    private int difficulty = 0;


    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameDifficulty.gameDifficulty;

        // Indecies for categores
        indexArray = new int[] { tabletsIndex, calculatorsIndex, notebooksIndex, laptopsIndex };
        inventoryObjectsArray = new GameObject[] { tabInventory , calcInventory, notebookInventory, laptopInventory };

        xTabletsUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutTablets;
        xCalculatorsUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutCalculators;
        xNotebooksUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutNotebooks;
        xLaptopsUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutLaptops;

        xObjectsArrayUI = new GameObject[] { xTabletsUI, xCalculatorsUI, xNotebooksUI, xLaptopsUI };

        // Adjust UI and shelves for difficulty level
        // Easy
        if (difficulty == 0)
        {
            numToDeactivate = 2;
        }
        // Medium
        if (difficulty == 1)
        {
            numToDeactivate = 3;
        }
        // Hard
        if (difficulty == 2)
        {
            numToDeactivate = 4;
        }

        hideSome(tablets);
        hideSome(calculators);
        hideSome(notebooks);
        hideSome(laptops);

        hideInventory(tabInventory);
        hideInventory(calcInventory);
        hideInventory(notebookInventory);
        hideInventory(laptopInventory);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        setTime.text = "Time: " + currentTime.ToString("F2") + " seconds";
        checkIfWon();
    }

    IEnumerator placeThisObject(GameObject objects)
    {
        foreach (int shelfCategory in indexArray)
        {
            if (objects.tag == shelfTagNames[shelfCategory])
            {
                // Activate enable object in storage shelf
                objects.transform.GetChild(categoryCounters[shelfCategory]).gameObject.SetActive(true);
                // Disable inventory icon on UI
                inventoryObjectsArray[shelfCategory].transform.GetChild(categoryCounters[shelfCategory]).gameObject.SetActive(false);
                xObjectsArrayUI[shelfCategory].transform.GetChild(categoryCounters[shelfCategory]).gameObject.SetActive(false);
                categoryCounters[shelfCategory]++;

                // If finished collecting 
                if (categoryCounters[shelfCategory] == numToDeactivate)
                {
                    // Only allow a full shelf to enable AJ category switch once
                    if (!checkedT)
                    {
                        // Disable current category dialogue and allow new category to be picked
                        GameObject.FindWithTag("AJ").GetComponent<standingAJ>().ajDialogueCategoriesFlags[shelfCategory] = false;
                        GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canPickNextCategory = true;
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == shelfTagNames[tabletsIndex])
        {
            // Add item to shelf if player has some
            if (amountCollected[tabletsIndex] > 0)
            {
                amountCollected[tabletsIndex]--;
                _AudioSource.Play();
                placedObject = placeThisObject(tablets);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No tablets collected");
            }
        }

        if (collision.gameObject.tag == shelfTagNames[calculatorsIndex])
        {
            // Add item to shelf if player has some
            if (amountCollected[calculatorsIndex] > 0)
            {
                amountCollected[calculatorsIndex]--;
                _AudioSource.Play();
                placedObject = placeThisObject(calculators);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No calculators collected");
            }
        }

        if (collision.gameObject.tag == shelfTagNames[notebooksIndex])
        {
            // Add item to shelf if player has some
            if (amountCollected[notebooksIndex] > 0)
            {
                amountCollected[notebooksIndex]--;
                _AudioSource.Play();
                placedObject = placeThisObject(notebooks);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No notebooks collected");
            }
        }

        if (collision.gameObject.tag == shelfTagNames[laptopsIndex])
        {
            // Add item to shelf if player has some
            if (amountCollected[laptopsIndex] > 0)
            {
                amountCollected[laptopsIndex]--;
                _AudioSource.Play();
                placedObject = placeThisObject(laptops);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No laptops collected");
            }
        }
    }

    public void hideSome(GameObject objectGroup)
    {
        for (int i = 0; i < numToDeactivate; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(i).gameObject;
            hideObject.SetActive(false);
        }
    }

    public void hideInventory(GameObject objectGroup)
    {
        for (int i = 0; i < 4 - numToDeactivate; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(3 - i).gameObject;
            hideObject.SetActive(false);
        }
    }

    public void checkIfWon()
    {
        if (categoryCounters[tabletsIndex] == numToDeactivate)
        {
            if (categoryCounters[calculatorsIndex] == numToDeactivate)
            {
                if (categoryCounters[notebooksIndex] == numToDeactivate)
                {
                    if (categoryCounters[laptopsIndex] == numToDeactivate)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().wonGame = true;
                    }
                }
            }
        }
    }
}
