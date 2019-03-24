using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class standingNPC : MonoBehaviour
{
    Animator animator;
    public AudioSource _AudioSource;
    public AudioClip _AudioClip1;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //_AudioSource.clip = _AudioClip1;
            _AudioSource.Play();
        }
    }

    void OnTriggerStay(Collider collision)
    {

    }

    void OnTriggerExit(Collider collision)
    {

    }
}
