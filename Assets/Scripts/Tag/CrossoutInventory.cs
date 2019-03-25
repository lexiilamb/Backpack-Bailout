using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossoutInventory : MonoBehaviour
{
    public GameObject xOutTablets;
    public GameObject xOutCalculators;
    public GameObject xOutNotebooks;
    public GameObject xOutLaptops;

    public int xTabCounter = 0;
    public int xCalcCounter = 0;
    public int xNotebookCounter = 0;
    public int xLaptopCounter = 0;

    public int numToActivate = 3;

    // Start is called before the first frame update
    void Start()
    {
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
