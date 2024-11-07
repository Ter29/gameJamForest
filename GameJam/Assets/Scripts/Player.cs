using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] private float moveSpeed = 4f;
    private float lookSpeed = 2f;
    private CharacterController controller;
    private float verticalRotation = 0f;
    [SerializeField] private Transform flashlight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        UpdateFlashlight();
    }
    private void Movement()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = 2f;
        }
        else{
            moveSpeed = 4f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up * mouseX);

        // Обмеження вертикального обертання
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -65f, 65f);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
    private void UpdateFlashlight()
    {
        if (flashlight != null)
        {
            // Match the flashlight's rotation to the camera's rotation
            flashlight.rotation = Camera.main.transform.rotation;

            // Optional: Apply an offset position to the flashlight if needed
            flashlight.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        }
    }
}
