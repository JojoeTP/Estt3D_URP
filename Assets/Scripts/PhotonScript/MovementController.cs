using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float lookSensitivity = 3f;

    [SerializeField]
    GameObject fpsCamera;

    Vector3 velocity = Vector3.zero;

    Vector3 rotation = Vector3.zero;

    float CameraUpAndDownRotation = 0f;
    float currentCameraUpAndDownRotation = 0f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // MoveInput
        float _xMoveMent = Input.GetAxisRaw("Horizontal");
        float _zMoveMent = Input.GetAxisRaw("Vertical");

        Vector3 _movementHorizontal = transform.right * _xMoveMent;
        Vector3 _movementVertical = transform.forward * _zMoveMent;
    
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * speed;
    
        Move(_movementVelocity);

        //Rotate
        float _yRotation = Input.GetAxis("Mouse X");
        Vector3 _rotationVector = new Vector3(0,_yRotation,0) * lookSensitivity;

        Rotate(_rotationVector);

        //cal look camera
        float _cameraUpAndDownRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

        RotateCamera(_cameraUpAndDownRotation);
    
    }

    // void PlayerInput(){

    // }

    public int maxAngleCamera;

    private void FixedUpdate() {
        if(velocity != Vector3.zero){
            rb.MovePosition(rb.position + (velocity *Time.fixedDeltaTime));
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if(fpsCamera != null){
            currentCameraUpAndDownRotation -= CameraUpAndDownRotation;
            currentCameraUpAndDownRotation = Mathf.Clamp(currentCameraUpAndDownRotation, -maxAngleCamera,maxAngleCamera);
            
            fpsCamera.transform.localEulerAngles = new Vector3(currentCameraUpAndDownRotation,0,0);
        }
    }

    void Move(Vector3 movementVelocity){
        velocity = movementVelocity;
    }

    void Rotate(Vector3 rotationVector){
        rotation = rotationVector;
    }

    void RotateCamera(float cameraUpAndDownRotation){
        CameraUpAndDownRotation = cameraUpAndDownRotation;
    }

}
