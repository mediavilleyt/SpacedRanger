using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    public InputActionAsset playerInput;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        //input using controller right stick
        float mouseX = playerInput.actionMaps[0].actions[1].ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = playerInput.actionMaps[0].actions[1].ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        //rotate the player body
        playerBody.Rotate(Vector3.up * mouseX);

        //rotate the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent the player from looking too far up or down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}