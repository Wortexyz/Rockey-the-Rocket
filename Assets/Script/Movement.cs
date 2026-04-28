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
    [SerializeField] ParticleSystem ThrustParticle,LeftThrustParticle,RightThrustParticle;
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
            ProcessTrustStart();
        }

        else
        {
            ProcessThrustStop();
        }
        if (rotation.IsPressed())
        {
            float rotationInput = rotation.ReadValue<float>();
            Debug.Log("Current Rotation is " + rotationInput);
            if (rotationInput > 0.0f)
            {
                ProcessLeftRotationStart();
            }
            else if (rotationInput < 0.0f)
            {
                ProcessRightRotationStart();
            }
        }
        else
        {
            ProcessRotationStop();
        }
    }
    private void ProcessTrustStart()
    {
        rb.AddRelativeForce(Vector3.up * force * Time.fixedDeltaTime);



        if (!ThrustAudio.isPlaying)
        {
            ThrustAudio.Play();
        }

        if (!ThrustParticle.isPlaying)
        {
            ThrustParticle.Play();
        }
    }
   private void ProcessThrustStop()
    {
        ThrustAudio.Stop();
        ThrustParticle.Stop();
    }
    private void RotationProcess( float forceToRotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, forceToRotate * Time.fixedDeltaTime);
        rb.freezeRotation= false;
    }
    private void ProcessRightRotationStart()
    {
        RotationProcess(RotationForce);
        if (!RightThrustParticle.isPlaying)
        {
            LeftThrustParticle.Stop();
            RightThrustParticle.Play();
        }
    }

    private void ProcessLeftRotationStart()
    {
        RotationProcess(-RotationForce);
        if (!LeftThrustParticle.isPlaying)
        {
            RightThrustParticle.Stop();
            LeftThrustParticle.Play();

        }
    }

    private void ProcessRotationStop()
    {
        LeftThrustParticle.Stop();
        RightThrustParticle.Stop();
    }
}
