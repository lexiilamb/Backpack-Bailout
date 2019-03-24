using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _rotateSpeed = 0;

    // when true, character can't move
    private bool altToggle = false;
    public bool changeAltToggle = false;

    private CharacterController _characterController;
    public Camera _camera;

    void Start ()
    {
		_characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update ()
    {
        if(!altToggle)
        {
            Vector3 moveDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            _characterController.SimpleMove(moveDir * _moveSpeed);

            float yRot = Input.GetAxis("Mouse X") * _rotateSpeed;
            float xRot = Input.GetAxis("Mouse Y") * _rotateSpeed;
            transform.Rotate(0, yRot, 0);
            _camera.transform.Rotate(-xRot, 0, 0);

            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;
        }

        if (changeAltToggle)
        {
            if (!altToggle)
            {
                Cursor.lockState = CursorLockMode.None;
                altToggle = true;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                altToggle = false;
            }

            changeAltToggle = false;
        }

        /*
        if (Input.GetButtonDown("Fire2"))
        {
            if(!altToggle)
            {
                Cursor.lockState = CursorLockMode.None;
                altToggle = true;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                altToggle = false;
            }
        }
        */
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pong")
        {
            altToggle = true;
        }
    }

}
