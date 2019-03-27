using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public float currentTime = 0.0f;
    public Text setTime;

    // Storage objects
    public GameObject tablets;
    public GameObject calculators;
    public GameObject notebooks;
    public GameObject laptops;
    private IEnumerator placedObject;

    //Inventory objects
    public GameObject tabInventory;
    public GameObject calcInventory;
    public GameObject notebookInventory;
    public GameObject laptopInventory;

    public GameObject xTabletsUI;
    public GameObject xCalculatorsUI;
    public GameObject xNotebooksUI;
    public GameObject xLaptopsUI;

    // Indecies 
    public int tabletCounter;
    public int calculatorCounter;
    public int notebookCounter;
    public int laptopCounter;

    // Collected objects
    public int numOfTablets = 0;
    public int numOfCalculators = 0;
    public int numOfNotebooks = 0;
    public int numOfLaptops = 0;

    // Boolean for CollectObject script 
    public bool canCollectObject = true;
    private int numToDeactivate = 3;

    // Check once for shelves being full for AJ script
    private bool checkedT = false;
    private bool checkedC = false;
    private bool checkedN = false;
    private bool checkedL = false;


    // Start is called before the first frame update
    void Start()
    {
        tabletCounter = 0;
        calculatorCounter = 0;
        notebookCounter = 0;
        laptopCounter = 0;
        canCollectObject = true;

        hideAll(tablets);
        hideAll(calculators);
        hideAll(notebooks);
        hideAll(laptops);

        xTabletsUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutTablets;
        xCalculatorsUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutCalculators;
        xNotebooksUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutNotebooks;
        xLaptopsUI = GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().xOutLaptops;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfWon();
        currentTime += Time.deltaTime;
        setTime.text = "Time: " + currentTime;
    }

    IEnumerator placeThisObject(GameObject objects)
    {
        // Allow another object to be collected later 
        canCollectObject = true;

        if (objects.tag == "TabletShelf")
        {
            // Activate enable object in storage shelf
            objects.transform.GetChild(tabletCounter).gameObject.SetActive(true);
            // Disable inventory icon on UI
            tabInventory.transform.GetChild(tabletCounter + 1).gameObject.SetActive(false);
            xTabletsUI.transform.GetChild(tabletCounter).gameObject.SetActive(false);
            tabletCounter++;

            // If finished collecting 
            if (tabletCounter == numToDeactivate)
            {
                // Only allow a full shelf to enable AJ category switch once
                if (!checkedT)
                {
                    // Disable current category dialogue and allow new category to be picked
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().findTables = false;
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canPickNextCategory = true;
                }
            }
        }

        if (objects.tag == "CalculatorShelf")
        {
            // Activate enable object in storage shelf
            objects.transform.GetChild(calculatorCounter).gameObject.SetActive(true);
            // Disable inventory icon on UI
            calcInventory.transform.GetChild(calculatorCounter + 1).gameObject.SetActive(false);
            xCalculatorsUI.transform.GetChild(calculatorCounter).gameObject.SetActive(false);
            calculatorCounter++;

            // If finished collecting 
            if (calculatorCounter == numToDeactivate)
            {
                // Only allow a full shelf to enable AJ category switch once
                if (!checkedC)
                {
                    // Disable current category dialogue and allow new category to be picked
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().findCalculators = false;
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canPickNextCategory = true;
                }
            }
        }

        if (objects.tag == "NotebookShelf")
        {
            // Activate enable object in storage shelf
            objects.transform.GetChild(notebookCounter).gameObject.SetActive(true);
            // Disable inventory icon on UI
            notebookInventory.transform.GetChild(notebookCounter + 1).gameObject.SetActive(false);
            xNotebooksUI.transform.GetChild(notebookCounter).gameObject.SetActive(false);
            notebookCounter++;

            // If finished collecting 
            if (notebookCounter == numToDeactivate)
            {
                // Only allow a full shelf to enable AJ category switch once
                if (!checkedN)
                {
                    // Disable current category dialogue and allow new category to be picked
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().findNotebooks = false;
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canPickNextCategory = true;
                }
            }
        }

        if (objects.tag == "LaptopShelf")
        {
            // Activate enable object in storage shelf
            objects.transform.GetChild(laptopCounter).gameObject.SetActive(true);
            // Disable inventory icon on UI
            laptopInventory.transform.GetChild(laptopCounter + 1).gameObject.SetActive(false);
            xLaptopsUI.transform.GetChild(laptopCounter).gameObject.SetActive(false);
            laptopCounter++;

            // If finished collecting 
            if (laptopCounter == numToDeactivate)
            {
                // Only allow a full shelf to enable AJ category switch once
                if (!checkedL)
                {
                    // Disable current category dialogue and allow new category to be picked
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().findLaptops = false;
                    GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canPickNextCategory = true;
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "TabletShelf")
        {
            // Add item to shelf if player has some
            if (numOfTablets > 0)
            {
                numOfTablets--;
                placedObject = placeThisObject(tablets);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No tablets collected");
            }
        }

        if (collision.gameObject.tag == "CalculatorShelf")
        {
            // Add item to shelf if player has some
            if (numOfCalculators > 0)
            {
                numOfCalculators--;
                placedObject = placeThisObject(calculators);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No calculators collected");
            }
        }

        if (collision.gameObject.tag == "NotebookShelf")
        {
            // Add item to shelf if player has some
            if (numOfNotebooks > 0)
            {
                numOfNotebooks--;
                placedObject = placeThisObject(notebooks);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No notebooks collected");
            }
        }

        if (collision.gameObject.tag == "LaptopShelf")
        {
            // Add item to shelf if player has some
            if (numOfLaptops > 0)
            {
                numOfLaptops--;
                placedObject = placeThisObject(laptops);
                StartCoroutine(placedObject);
            }
            else
            {
                Debug.Log("No laptops collected");
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {

    }

    public void hideAll(GameObject objectGroup)
    {
        for (int i = 0; i < numToDeactivate; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(i).gameObject;
            hideObject.SetActive(false);
        }

    }

    public void checkIfWon()
    {
        if (tabletCounter == numToDeactivate)
        {
            if (calculatorCounter == numToDeactivate)
            {
                if (notebookCounter == numToDeactivate)
                {
                    if (laptopCounter == numToDeactivate)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().gameWon();
                    }
                }
            }
        }
    }
}
