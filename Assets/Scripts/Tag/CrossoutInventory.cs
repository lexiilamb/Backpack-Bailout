using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossoutInventory : MonoBehaviour
{
    public GameObject xOutTablets;
    public GameObject xOutCalculators;
    public GameObject xOutNotebooks;
    public GameObject xOutLaptops;

    public int xTabCounter;
    public int xCalcCounter;
    public int xNotebookCounter;
    public int xLaptopCounter;

    public int numToActivate;

    // Start is called before the first frame update
    void Start()
    {
        numToActivate = 4;

        xTabCounter = xCalcCounter = xNotebookCounter = xLaptopCounter = 0;

        hideAllUI(xOutTablets);
        hideAllUI(xOutCalculators);
        hideAllUI(xOutNotebooks);
        hideAllUI(xOutLaptops);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideAllUI(GameObject objectGroup)
    {
        for (int i = 0; i < numToActivate; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(i).gameObject;
            hideObject.SetActive(false);
        }

    }

    public void crossoutTablets()
    {
        xOutTablets.transform.GetChild(xTabCounter).gameObject.SetActive(true);
        xTabCounter++;
    }

    public void crossoutCalculators()
    {
        xOutCalculators.transform.GetChild(xCalcCounter).gameObject.SetActive(true);
        xCalcCounter++;
    }
    public void crossoutNotebooks()
    {
        xOutNotebooks.transform.GetChild(xNotebookCounter).gameObject.SetActive(true);
        xNotebookCounter++;
    }
    public void crossoutLaptops()
    {
        xOutLaptops.transform.GetChild(xLaptopCounter).gameObject.SetActive(true);
        xLaptopCounter++;
    }
}
