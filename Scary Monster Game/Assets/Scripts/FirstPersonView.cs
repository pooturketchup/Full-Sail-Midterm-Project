using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Source were code was obtained - Brackeys - First Person Movement in Unity- FPS Controller: 
//https://www.youtube.com/watch?v=_QajrabyTJc&list=PLgaNWmJF3zopH7o334lf92y_ZqEd123gq&index=2
public class FirstPersonView : MonoBehaviour
{
    public float mouseSens;// = 100f;
    public Transform playerbody;
    //public SoundDrop soundDrop;
    private float transitionTime = 1.0F;
    private float startTime;
    private float speed = 1.0F;
   // private Vector3 velocity = Vector3.zero;
   [NonSerialized] public float xRotation = 0f;
    [NonSerialized] public bool playerRotation;
    public Transform[] itemView;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startTime = Time.time;
        transitionTime = Vector3.Distance(playerbody.position, new Vector3(playerbody.position.x, playerbody.position.y - 0.5F, playerbody.position.z)); 
        playerRotation = true;
    }

    // Update is called once per frame
    //Camera first person view
    void Update()
    {
        //player is able to move and rotate
        if (playerRotation == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX);

            //The camera needs to target the player position, The camera normal (standing) position
            this.transform.position = new Vector3(playerbody.position.x, playerbody.position.y + 1, playerbody.position.z);



            //Setting time for the transition between camera standing and crouch position
            float distCovered = (Time.time - startTime) * speed;
            float fractionJourney = distCovered / transitionTime;

            //checking if the player crouched lerping the camera here
            if (Input.GetButton("Crouch"))
            {
                //updated crouch view based on the player position
                Vector3 crouch = (new Vector3(this.transform.position.x, this.transform.position.y - 0.5F, this.transform.position.z));
                //Lerp between the normal and crouching positions
                this.transform.position = Vector3.Lerp(this.transform.position, crouch, fractionJourney);
            }
            else if(Input.GetButtonUp("Crouch"))
            {
                //updated crouch view based on the player position
                Vector3 crouch = this.transform.TransformPoint(new Vector3(this.transform.position.x, this.transform.position.y + 0.5F, this.transform.position.z));
                //Lerp between the normal and crouching positions
                this.transform.position = Vector3.Lerp(crouch, this.transform.position, fractionJourney);
            }

        }
        
    }
}
