using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    public AudioSource _AudioSource;

    private IEnumerator collectedObject;

    // Prevent objects from being counted twice
    private bool canDestroyObject = true;

    // Start is called before the first frame update
    void Start()
    {
        collectedObject = destroyObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator destroyObject()
    {
        canDestroyObject = false;
        // Increment a counter for object
        if (this.gameObject.tag == "Tablet")
        {
            FindObjectOfType<PlaceObject>().numOfTablets++;
        }
        if (this.gameObject.tag == "Calculators")
        {
            FindObjectOfType<PlaceObject>().numOfCalculators++;
        }
        if (this.gameObject.tag == "Notebooks")
        {
            FindObjectOfType<PlaceObject>().numOfNotebooks++;
        }
        if (this.gameObject.tag == "Laptop")
        {
            FindObjectOfType<PlaceObject>().numOfLaptops++;
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
                /*
                // Player must place each item in storage before collecting a new item
                if(FindObjectOfType<PlaceObject>().canCollectObject)
                {
                    FindObjectOfType<PlaceObject>().canCollectObject = false;
                    collectedObject = destroyObject();
                    StartCoroutine(collectedObject);
                }
                */
                // Check if tag can be collected
                if (this.gameObject.tag == "Tablet" && GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canCollectTablets)
                {
                    GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().crossoutTablets();
                    startDestroyCoroutine();
                }
                if (this.gameObject.tag == "Calculators" && GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canCollectCalculators)
                {
                    GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().crossoutCalculators();
                    startDestroyCoroutine();
                }
                if (this.gameObject.tag == "Notebooks" && GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canCollectNotebooks)
                {
                    GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().crossoutNotebooks();
                    startDestroyCoroutine();
                }
                if (this.gameObject.tag == "Laptop" && GameObject.FindWithTag("AJ").GetComponent<standingAJ>().canCollectLaptops)
                {
                    GameObject.FindWithTag("inventoryUI").GetComponent<CrossoutInventory>().crossoutLaptops();
                    startDestroyCoroutine();
                }
            }
        }
    }
}
