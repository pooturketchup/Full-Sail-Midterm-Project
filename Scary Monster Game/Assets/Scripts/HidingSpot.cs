using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HidingSpot : MonoBehaviour
{
    public PlayerBehavior player;
    public new FirstPersonView camera;
    [NonSerialized] public bool hide = false;
    public Transform leaveHidingSpot;
    public Transform peek;
    bool play = false;
    public UINotificationText text;
    private Vector3 distance;

    private void Start()
    {
        distance = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(hide);
        //player has to hold e to hide
        if (Input.GetButtonDown("Trigger"))//Down
        {
            hide = !hide;//false;
        }
        //else if(Input.GetButton("Trigger"))//UP
        //{
        //    hide = true;
        //}


        if (Vector3.Distance(distance, player.transform.position) < 2.5F)
        {
            if(hide == false && player.pressedButton == true)
            {
                text.timer = 0F;
            }
            else
            {
                //display that the player pickup the item to the UI
                text.text = "Hold [LeftClick] to Hide";
                text.timer = 0.05F;
            }
        }
        else
        {
            text.show = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //checks for the player
        if (other.gameObject.tag == "Player")
        {
            //checks to see if the player wanted to hide
           if(player.hide == true)
            {

                if (hide == false)
                {
                    if(play == true)
                    {
                        FindObjectOfType<AudioManager>().Play("Locker");
                        play = false;
                    }
                    //hides the player inside the object;
                    camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2.7F, this.transform.position.z); // 3.4F 2.7F

                    player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2.7F, this.transform.position.z);
                    //make the player turn around to face the front of the hiding spot (looking at the door)
                    camera.transform.LookAt(peek);

                    //Make the playerbody unable to move or rotate
                    player.playerMovement = false;
                    camera.playerRotation = false;

                    //deactivate the player collider
                    player.GetComponent<CapsuleCollider>().enabled = false;

                    camera.GetComponent<FirstPersonView>().enabled = false;
                }
                else if (hide == true)
                {
                    //play exit audio here
                    FindObjectOfType<AudioManager>().Play("Locker");
                    //place the player outside of the hiding spot
                    player.transform.position = new Vector3(leaveHidingSpot.transform.position.x, leaveHidingSpot.position.y, leaveHidingSpot.position.z);

                    //let the player move and rotate again
                    player.playerMovement = true;
                    camera.playerRotation = true;
                    //activate the player collider
                    player.GetComponent<CapsuleCollider>().enabled = true;

                    camera.GetComponent<FirstPersonView>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        play = true;
    }

    private void OnTriggerExit(Collider other)
    {
        play = true;
    }
}
