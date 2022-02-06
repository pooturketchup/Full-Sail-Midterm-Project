using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Tutorial name First Person Movement in Unity - FPS Controller by Brackeys
    //link to tutorial: https://www.youtube.com/watch?v=_QajrabyTJc

    public CharacterController control;
    public float movementSpeed = 12f;
    public float weight = -9.81f;
    public float jump = 3f;

    public Transform checkGround;
    public float distanceGround = 0.4f;
    public LayerMask floorMask;

    Vector3 velocitySpeed;
    bool grounded;

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.CheckSphere(checkGround.position, distanceGround, floorMask);

        if(grounded && velocitySpeed.y < 0)
        {
            velocitySpeed.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        control.Move(move * movementSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocitySpeed.y = Mathf.Sqrt(jump * -2f * weight);
        }

        velocitySpeed.y += weight * Time.deltaTime;

        control.Move(velocitySpeed * Time.deltaTime);
    }
}
