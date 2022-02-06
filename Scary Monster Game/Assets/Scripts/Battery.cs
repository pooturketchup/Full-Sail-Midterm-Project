using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public PlayerBehavior player;
    public Flashlight flashlight;
    private MeshRenderer visible;
    public UIPickUpText text;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //continues to update the battery position and turns of shadow
        if(player.batteries.Count == 1)
        {
            visible = player.batteries[0].GetComponentInChildren<MeshRenderer>();
            player.batteries[0].transform.position = player.transform.position;
            visible.enabled = false;
        }
        else if (player.batteries.Count == 2)
        {
            visible = player.batteries[1].GetComponentInChildren<MeshRenderer>();
            player.batteries[1].transform.position = player.transform.position;
            visible.enabled = false;
        }
        else if(player.batteries.Count == 3)
        {
            visible = player.batteries[2].GetComponentInChildren<MeshRenderer>();
            player.batteries[2].transform.position = player.transform.position;
            visible.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if the player is within the radius they can pick it up
        if(other.gameObject.tag == "Player")
        {
            //checks to see if the player tried to pickup the battery
            if (player.pressedButton == true)
            {
                //allows the player to hold 3 batteries at a time
                if (player.batteries.Count < 3)
                {
                    if (this.transform.position != player.transform.position)
                    {   
                        //play audio here
                        FindObjectOfType<AudioManager>().Play("BatteryPickUp");
                        AddBattery(this, player.batteries);
                        //display that the player pickup the item to the UI
                        text.text = "Added Battery...";
                        text.timer = 3.0F;
                    }
                }
            }
        }
    }

    public void AddBattery(Battery battery, List<Battery> list)
    {
        if (!list.Contains(battery))
        {
            list.Add(battery);
        }
        flashlight.batteryCheck = battery;
    }
}
