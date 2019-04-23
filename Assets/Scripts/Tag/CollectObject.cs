using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    // Indecies for categories 
    private int tabletsIndex = 0;
    private int calculatorsIndex = 1;
    private int notebooksIndex = 2;
    private int laptopsIndex = 3;
    private int[] indexArray;

    private string[] tagNames = new string[] { "Tablet", "Calculators", "Notebooks", "Laptop"};


    public AudioSource _AudioSource;

    private IEnumerator collectedObject;

    // Prevent objects from being counted twice
    private bool canDestroyObject = true;

    // Start is called before the first frame update
    void Start()
    {
        // Indecies for categores
        indexArray = new int[] { tabletsIndex, calculatorsIndex, notebooksIndex, laptopsIndex };

        collectedObject = destroyObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator destroyObject()
    {
        canDestroyObject = false;

        foreach (int item in indexArray)
        {
            // Increment a counter for object
            if (this.gameObject.tag == tagNames[item])
            {
                Debug.Log("Item index: " + item);
                Debug.Log("Destroying: " + tagNames[item]);
                FindObjectOfType<PlaceObject>().amountCollected[item]++;
            }
        }

        _AudioSource.Play();

        // Do collection aniamtion/effect
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        // Destroy effect
        Destroy(this.transform.GetChild(0).gameObject);
        // Destroy object
        Destroy(this.gameObject);
        canDestroyObject = true;
    }

    public void startDestroyCoroutine()
    {
        collectedObject = destroyObject();
        StartCoroutine(collectedObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Prevents multiple collection of the same object
            if (canDestroyObject)
            {
                foreach (int item in indexArray)
                {
                    if (this.gameObject.tag == tagNames[item] && GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canCollectFlags[item])
                    {
                        Debug.Log("Collected: " + tagNames[item]);
                        GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().crossout(item);
                        startDestroyCoroutine();
                    }
                }
            }
        }
    }
}
