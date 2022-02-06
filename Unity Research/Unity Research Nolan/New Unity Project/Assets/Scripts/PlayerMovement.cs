using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Source were code was obtained - Brackeys - First Person Movement in Unity- FPS Controller:
//https://www.youtube.com/watch?v=_QajrabyTJc&list=PLgaNWmJF3zopH7o334lf92y_ZqEd123gq&index=2


public class PlayerMovement : MonoBehaviour
{
    public CharacterController player;

    public float speed = 5f;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(move * speed * Time.deltaTime);
    }
}
