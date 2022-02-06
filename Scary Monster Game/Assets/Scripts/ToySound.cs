using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToySound : MonoBehaviour
{
    public new AudioSource audio;
    public PlayerBehavior player;
    private MeshRenderer visible;
    public Transform placement;
    public UIPickUpText text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates toys position to the player and makes it invisible
        if(player.toys.Count == 1)
        {
            visible = player.toys[0].GetComponent<MeshRenderer>();
            player.toys[0].transform.position = player.transform.position;
            visible.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //plays audio so that the player can know if the monster was nearby
        if(other.gameObject.tag == "Monster" && this.transform.position != player.transform.position)
        {
            //play audio here
            if(audio.isPlaying == false)
            {
                audio.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if the player is within radius the player can pick up the toy
        if(other.gameObject.tag == "Player")
        {
            //only allow the player to carry one at a time
            if(player.toys.Count < 1)
            {
                if(this.transform.position != player.transform.position)
                {
                    //checks to see if the player tried to pickup the bag
                    if(player.pressedButton == true)
                    {
                        FindObjectOfType<AudioManager>().Play("ToyPickUp");
                        player.toys.Add(this);
                        //display text on UI
                        text.text = "Added Toy...";
                        text.timer = 3.0F;
                    }
                }
            }
            else if(player.toys.Count == 1)
            {
                //allows the player to place down the toy 
                if (player.placeDown == true)
                {

                    //place at the transform in front of the player
                    this.transform.position = placement.transform.position;

                    //play audio
                    FindObjectOfType<AudioManager>().Play("ToyPickUp");
                    //turn the meshrenderer back on
                    visible.enabled = true;

                    //remove the toy from the player inventory
                    player.toys.Remove(this);
                    //display text on UI
                    text.text = "Removed Toy...";
                    text.timer = 3.0F;
                }
            }
        }
    }
}
