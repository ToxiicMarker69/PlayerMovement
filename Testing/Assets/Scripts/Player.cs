using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float verticalVelocity;
    private float gravity = 12.0f;
    private float jumpForce = 3.0f;
    //[SerializeField] RuntimeData _runtimeData;
    [SerializeField] float _mouseSensitivity = 10f;

    [SerializeField] float _moveSpeed = 6f;
    [SerializeField] Camera _cam;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    float _currentTilt = 0f;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        //if (_runtimeData.CurrentGameplayState == GameplayState.FreeWalk)
        Movement();
            
        
       
        
        //_cam.transform.localEulerAngles
        //_cam.transform.Rotate(Vector3.right, mouseY * _mouseSensitivity);
    
    }
    

    void Aim()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        

        transform.Rotate(Vector3.up, mouseX * _mouseSensitivity);
        
        _currentTilt -= mouseY;
        _currentTilt = Mathf.Clamp(_currentTilt, -45,45);
        _cam.transform.localEulerAngles = new Vector3(_currentTilt, 0, 0);
    }
    void Movement()
    {
        if(controller.isGrounded)
        {  
            verticalVelocity = -gravity * Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }   

        Vector3 sidewaysMovementVector = transform.right * Input.GetAxis("Horizontal");
        Vector3 upDownMovementVector = transform.up * verticalVelocity;
        Vector3 forwardBackwardMovementVector = transform.forward * Input.GetAxis("Vertical");
        Vector3 movementVector = sidewaysMovementVector + upDownMovementVector +forwardBackwardMovementVector;

    
        GetComponent<CharacterController>().Move(movementVector * _moveSpeed * Time.deltaTime); 
        
           
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }
}
