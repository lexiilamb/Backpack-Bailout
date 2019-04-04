using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollect : MonoBehaviour
{
    public AudioSource _AudioSource;

    public GameObject xOutLaptopsUI;

    private IEnumerator collectedObject;

    // Prevent objects from being counted twice
    private bool canDestroyObject = true;

    // Start is called before the first frame update
    void Start()
    {
        collectedObject = destroyObject();
        xOutLaptopsUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator destroyObject()
    {
        canDestroyObject = false;
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
            // Collect object on collision
            startDestroyCoroutine();
        }
    }
}
