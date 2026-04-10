using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
   private  Rigidbody rb;
    [SerializeField] float force=10f, RotationForce = 10f;
    AudioSource ThrustAudio;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       ThrustAudio = GetComponent<AudioSource>();   
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()

    {
        InputActions();
    }

    private void InputActions()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * force * Time.fixedDeltaTime);
           if(!ThrustAudio.isPlaying)
            {
                ThrustAudio.Play();
            }
           
        }

        else
        {
            ThrustAudio.Stop();
        }
        if (rotation.IsPressed())
        {
            float rotationInput = rotation.ReadValue<float>();
            Debug.Log("Current Rotation is " + rotationInput);
            if (rotationInput > 0.0f)
            {
                RotationProcess(-RotationForce);
            }
            else if (rotationInput < 0.0f)
            {
                RotationProcess(RotationForce);
            }
        }
    }

    private void RotationProcess( float forceToRotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, forceToRotate * Time.fixedDeltaTime);
        rb.freezeRotation= false;
    }
}
