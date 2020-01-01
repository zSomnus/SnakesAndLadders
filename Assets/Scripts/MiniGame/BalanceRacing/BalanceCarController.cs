using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceCarController : MonoBehaviour
{
    // Reference
    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
    public Rigidbody2D rb;
    public Joystick joystick;
    
    public float speed = 1500;
    public float rotationSpeed = 15;

    private float movement = 0f;
    private float rotation = 0f;


    // Update is called once per frame
    void Update()
    {
        rotation = joystick.Horizontal * rotationSpeed;
    }

    public void Boost()
    {
        movement = -1 * speed;
    }

    public void StopBoost()
    {
        movement = 0;
    }

    void FixedUpdate()
    {
        if (Math.Abs(movement) < Mathf.Epsilon)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            JointMotor2D motor = new JointMotor2D {motorSpeed = movement, maxMotorTorque = backWheel.motor.maxMotorTorque};
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }

        rb.AddTorque(-rotation * rotationSpeed*Time.fixedDeltaTime);

    }
}
