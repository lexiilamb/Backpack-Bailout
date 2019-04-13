using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _rotateSpeed = 0;

    float yRot;
    float xRot;

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
            //Vector3 moveDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            float horizInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            Vector3 forwardMove = transform.forward * vertInput;
            Vector3 rightMove = transform.right * horizInput;

            _characterController.SimpleMove(Vector3.ClampMagnitude(forwardMove + rightMove, 1.0f) * _moveSpeed);

            //_characterController.SimpleMove(moveDir * _moveSpeed);

            yRot += Input.GetAxis("Mouse X") * _rotateSpeed;
            xRot += Input.GetAxis("Mouse Y") * _rotateSpeed;

            Debug.Log("yRot: " + yRot);
            Debug.Log("xRot: " + xRot);

            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(0, yRot, 0);
            _camera.transform.localRotation = Quaternion.Euler(-xRot, 0, 0);

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
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Pong")
        {
            altToggle = true;
        }
    }

}
