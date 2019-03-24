using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
    private Rigidbody player;
    public AudioSource _AudioSource;

    public AudioClip _AudioClip1;
    public AudioClip _AudioClip2;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
        _AudioSource.clip = _AudioClip1;
        _AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Must use "Collider" as variable for trigger functions
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            //_AudioSource.clip = _AudioClip2;
            //_AudioSource.Play();
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            //_AudioSource.clip = _AudioClip1;
            //_AudioSource.Play();
        }

    }
}
