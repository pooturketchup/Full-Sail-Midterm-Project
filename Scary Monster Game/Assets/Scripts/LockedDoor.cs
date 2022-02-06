using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public string doorCode;
    public PlayerBehavior player;
    public Key key;
    private float yrotation;
    private bool opened = false;
    public UINotificationText text;
    private void Update()
    {
        //if (Vector3.Distance(this.transform.position, player.transform.position) < 2.5F)
        //{
        //    //if (player.pressedButton == true)
        //    //{
        //    //  text.show = false;
        //    //}
        //    //else
        //    //{
        //    //display that the player pickup the item to the UI
        //    text.text = doorCode + " Door (Locked)";
        //    text.timer = 0.05F;//true;
        //    //}
        //}
        //else
        //{
        //    text.timer = 0F;//show = false;
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        //if the player enters the trigger zone
        if(other.gameObject.tag == "Player")
        {
            //display that the player pickup the item to the UI
            if(yrotation == 100)
            {
                text.text = doorCode + " Door (Unlocked)";
            }
            else
            {
                text.text = doorCode + " Door (Locked)";
            }
            text.timer = 0.05F;
            //if the player has a key in their inventory
            if (player.keys.Contains(key))
            {
                //if the player pressed E
                if (player.pressedButton == true)
                {
                    //if the key matches the door
                    if (key.keyCode == doorCode)
                    {
                        //play audio here
                        if(opened == true)
                        FindObjectOfType<AudioManager>().Play("DoorOpen");
                        //makes the door rotate on its y axis player needs to hold E to open door fully
                        if (yrotation < 100)
                        {
                            yrotation += 10F;
                            this.transform.rotation = Quaternion.Euler(0, yrotation, 0);
                            opened = true;
                            text.text = doorCode + " Door (Unlocked)";
                        }
                        else if (yrotation == 100)
                        {
                            opened = false;
                        }
                       
                    }
                }
            }
        }
    }
}
