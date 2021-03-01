using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotController : MonoBehaviour{
    public float forwardSpeed =25f, strafeSpeed = 7.5f, diveSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeDiveSpeed;
    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

   
    // Start is called before the first frame update
    void Start()
    {

        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) /screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) /screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(-mouseDistance.y * lookRotateSpeed *Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, 0f, Space.Self);

        activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardSpeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeDiveSpeed = Input.GetAxisRaw("Dive") * diveSpeed;

        transform.position += (transform.forward * activeForwardSpeed * Time.deltaTime) + (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeDiveSpeed * Time.deltaTime);

    }
}
