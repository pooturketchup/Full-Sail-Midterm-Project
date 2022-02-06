using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyCode;
    public PlayerBehavior player;
    private MeshRenderer visible;
    public Transform[] spawnPoints;
    private Transform spawnKey;
    private bool carry = false;
    private bool playsound = false;
    public UIPickUpText text;
    private void Start()
    {
        //lets the key spawn at random locations that we set in the game world
        spawnKey = spawnPoints[Random.Range(0, spawnPoints.Length)];
        this.transform.position = spawnKey.position;
    }

    // Update is called once per frame
    void Update()
    {
            //checks to see if the player has a key on their person
            if (this.transform.position == player.transform.position)
            {
                visible = this.GetComponent<MeshRenderer>();
                visible.enabled = false;
            }
            if(carry == true)
            {
            this.transform.position = player.transform.position;
            playsound = true;
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(player.pressedButton == true)
            {
                //added check to properly updated UI text
                if(this.transform.position != player.transform.position)
                {
                    if (playsound == false)
                    {
                        //play audio here
                        FindObjectOfType<AudioManager>().Play("KeyPickUp");
                    }
                    //if the player tried to pick up the key change its position to the player
                    this.transform.position = player.transform.position;
                    carry = true;
                    //audio goes here
                    player.keys.Add(this);
                    //let the player know what key the picked up in the UI
                    text.text = "Added " + keyCode + " Key...";
                    text.timer = 3.0F;
                }
            }
        }
    }
}
