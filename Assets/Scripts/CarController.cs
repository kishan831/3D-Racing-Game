using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float currentsteerAngle;
    [SerializeField] private WheelCollider frontleftwheelcollider;
    [SerializeField] private WheelCollider frontRighttwheelcollider;
    [SerializeField] private WheelCollider rearleftwheelcollider;
    [SerializeField] private WheelCollider rearRightwheelcollider;
    [SerializeField] private Transform frontleftwheelTransform;
    [SerializeField] private  Transform frontRighttwheelTransform;
    [SerializeField] private Transform rearleftwheelTransform;
    [SerializeField] private Transform rearRightwheelTransform;

    public object Frontrightwheelcollider { get; private set;}

    private void Update()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
     {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
     }

    private void HandleMotor()
    {
        frontleftwheelcollider.motorTorque = verticalInput * motorForce;
        frontRighttwheelcollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;

        if(isBreaking)
        {
           ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        frontleftwheelcollider.brakeTorque = currentbreakForce;
        frontRighttwheelcollider.brakeTorque = currentbreakForce;
        rearleftwheelcollider.brakeTorque = currentbreakForce;
        rearRightwheelcollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
       currentsteerAngle = maxSteerAngle * horizontalInput;
       frontleftwheelcollider.steerAngle = currentsteerAngle;
       frontRighttwheelcollider.steerAngle = currentsteerAngle ;
    }

    private void UpdateWheels()

    {
       UpdateSinglewheel(frontleftwheelcollider,frontleftwheelTransform);
       UpdateSinglewheel(frontRighttwheelcollider,frontRighttwheelTransform);
       UpdateSinglewheel(rearleftwheelcollider,rearleftwheelTransform);
       UpdateSinglewheel(rearRightwheelcollider,rearRightwheelTransform);
    }

    private void UpdateSinglewheel(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }
    
}
