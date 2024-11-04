using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -1f;
    public Vector3 enterLoc = new Vector3(-0.639999986f,0,1.42999995f);
    public Quaternion enterRot = new Quaternion(0f,0f,0f,1f);
    public Vector3 exitLoc = new Vector3(-2.20000005f,0f,1.13f);
    public Quaternion exitRot = new Quaternion(0f,0f,0f,1f);
    public GameObject player;
    public GameObject car;
    public InputActionReference stick;

    WheelControl[] wheels;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        // Adjust center of mass vertically, to help prevent the car from rolling
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;

        // Find all child GameObjects that have the WheelControl script attached
        wheels = GetComponentsInChildren<WheelControl>();
    }

    public void PlayerEnter(){
        Debug.Log("PLAYER ENTER\n");
        player.transform.SetParent(car.transform);
        player.transform.position = enterLoc;
        player.transform.rotation = enterRot;
    }

    public void PlayerExit(){
        Debug.Log("PLAYER EXIT\n");
        player.transform.position = exitLoc;
        player.transform.rotation = exitRot;
        player.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // var inputDevices = new List<UnityEngine.XR.InputDevice>();
        // UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        // foreach (var device in inputDevices)
        // {
        //     Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        // }
        // rigidBody = GetComponent<Rigidbody>();

        // float vInput = Input.GetAxis("Vertical");
        // float hInput = Input.GetAxis("Horizontal");
        Vector2 input = stick.ToInputAction().ReadValue<Vector2>();
        float vInput = input.y;
        float hInput = input.x;        
        
        // Calculate current speed in relation to the forward direction of the car
        // (this returns a negative number when traveling backwards)
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);


        // Calculate how close the car is to top speed
        // as a number from zero to one
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        // Use that to calculate how much torque is available 
        // (zero torque at top speed)
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        // …and to calculate how much to steer 
        // (the car steers more gently at top speed)
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Check whether the user input is in the same direction 
        // as the car's velocity
        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            // Apply steering to Wheel colliders that have "Steerable" enabled
            if (wheel.steerable)
            {
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            }

            if (isAccelerating)
            {
                // Apply torque to Wheel colliders that have "Motorized" enabled
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0;
            }
            else
            {
                // If the user is trying to go in the opposite direction
                // apply brakes to all wheels
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheel.WheelCollider.motorTorque = 0;
            }
        }
    }
}
