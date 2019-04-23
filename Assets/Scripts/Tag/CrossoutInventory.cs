using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossoutInventory : MonoBehaviour
{
    // Indecies for categories 
    private int tabletsIndex = 0;
    private int calculatorsIndex = 1;
    private int notebooksIndex = 2;
    private int laptopsIndex = 3;
    private int[] indexArray;

    // X gameobjects
    public GameObject[] xArray; 
    public GameObject xOutTablets;
    public GameObject xOutCalculators;
    public GameObject xOutNotebooks;
    public GameObject xOutLaptops;

    // X out category counters
    public int[] xOutCategoryCounters = new int[] { 0, 0, 0, 0 };

    public int numToActivate;

    // Start is called before the first frame update
    void Start()
    {
        // Indecies for categores
        indexArray = new int[] { tabletsIndex, calculatorsIndex, notebooksIndex, laptopsIndex };
        // X's for the inventory UI
        xArray = new GameObject[] { xOutTablets, xOutCalculators, xOutNotebooks, xOutLaptops};

        numToActivate = 4;

        foreach (GameObject item in xArray)
        {
            hideAllUI(item);
        }
    }

    public void hideAllUI(GameObject objectGroup)
    {
        for (int i = 0; i < numToActivate; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(i).gameObject;
            hideObject.SetActive(false);
        }
    }

    public void crossout(int index)
    {
        xArray[index].transform.GetChild(xOutCategoryCounters[index]).gameObject.SetActive(true);
        xOutCategoryCounters[index]++;
    }
}
