using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 0;
    [SerializeField] private float _rotateSpeed = 0;

    float horizInput;
    float vertInput;
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
        Cursor.visible = false;
    }
	
	void Update ()
    {
        if(!altToggle)
        {
            //Vector3 moveDir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            horizInput = Input.GetAxis("Horizontal");
            vertInput = Input.GetAxis("Vertical");

            Vector3 forwardMove = transform.forward * vertInput;
            Vector3 rightMove = transform.right * horizInput;

            _characterController.SimpleMove(Vector3.ClampMagnitude(forwardMove + rightMove, 1.0f) * _moveSpeed);

            //_characterController.SimpleMove(moveDir * _moveSpeed);

            yRot += Input.GetAxis("Mouse X") * _rotateSpeed;
            xRot += Input.GetAxis("Mouse Y") * _rotateSpeed;

            if (Input.GetAxis("RightJoystickHorizontal") != 0 || Input.GetAxis("RightJoystickVertical") != 0)
            {
                yRot += Input.GetAxis("RightJoystickHorizontal") * _rotateSpeed;
                xRot += Input.GetAxis("RightJoystickVertical") * -(_rotateSpeed);
            }

            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.rotation = Quaternion.Euler(0, yRot, 0);
            _camera.transform.localRotation = Quaternion.Euler(-xRot, 0, 0);
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
