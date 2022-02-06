using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Flashbang : MonoBehaviour
{
    public PlayerBehavior player;
    public MonsterBehavior monster;
    private SphereCollider activate;
    private Vector3 playerPosition;
    [NonSerialized] public Flashbang flashbangCheck;
    private Flashbang temp;
   // private Flashbang flashbang = null;
    // private SphereCollider soundDrop;
    // private float fuseTime;
    [NonSerialized] public bool startFuse = false;
    //testing
    float throwForce = 1000;
    private Vector3 flashPos;
    public Transform throwPosition;
    public bool inInventory = false;
    public FlashSoundDrop drop;
    public float coolDown = 120F;
    private float resetCoolDown;
    [NonSerialized] public bool thrown = false;
    [NonSerialized] public bool acquired = false;
    //[NonSerialized] public List<Flashbang> currentUse = new List<Flashbang>();
    //if there is a flashbang in the players inventory set the flashbangs position to be one of the item holders in the UI
    private void Start()
    {
        //soundDrop = this.GetComponent<SphereCollider>();
        // soundDrop.enabled = false;

        //initially set the transform
        playerPosition = player.transform.position;
        flashPos = this.transform.position;
        resetCoolDown = this.GetComponent<Flashbang>().coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        //if the player has acquired the flashbang allow them to use it
        if(acquired == true)
        {
            //if the player has the flashbang off cooldown they can use the flashbang
            if (coolDown > 0)// && thrown == false)
            {
                //start the cool down again
                coolDown -= Time.deltaTime;
            }
            else
            {
                //once the cool down if overt the player can now throw the flashbang
                if (inInventory == false)
                {
                    //set flash to its own transform and detach from the throw position
                    flashPos = this.transform.position;
                    this.transform.SetParent(null);
                    this.GetComponent<Rigidbody>().useGravity = true;
                    this.transform.position = flashPos;
                }
                else //(inInventory == true)
                {
                    //set rigidbody values
                    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    this.transform.SetParent(throwPosition);

                    //if the player uses a flashbang throw the flashbang
                    if (Input.GetButtonDown("Trigger"))
                    {
                        Debug.Log("Flashbang Thrown boy");
                        if (player.flashbangs.Contains(this) && player.flashbangs.Count != 0)
                        {
                            //start the fuse timer
                            startFuse = true;
                            inInventory = false;

                            //throw
                            this.GetComponent<Rigidbody>().AddForce(throwPosition.forward * throwForce);


                            //put audio here throwing
                            FindObjectOfType<AudioManager>().Play("Throw");



                            //resets values and destroys the used flashbang delete in the flash sounddrop script
                            temp = this;
                            player.flashbangs.Remove(this);
                            // AssignFlashbangValues(flashbang, player.flashbangs);

                            //once thrown set thrown to false
                            //thrown = true;
                        }
                    }
                }

            }
        }








        //Debug.Log(thrown);
        Debug.Log(player.flashbangs.Count);
        playerPosition = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
     
        if(player.flashbangs.Count == 1)
        {
            activate = player.flashbangs[0].GetComponent<SphereCollider>();
            player.flashbangs[0].transform.position = playerPosition;//.position;
            activate.enabled = false;
        }
        else if(player.flashbangs.Count == 2)
        {
            activate = player.flashbangs[1].GetComponent<SphereCollider>();
            player.flashbangs[1].transform.position = playerPosition;//.position;
            activate.enabled = false;
        }
        else if (player.flashbangs.Count == 3)
        {
            activate = player.flashbangs[2].GetComponent<SphereCollider>();
            player.flashbangs[2].transform.position = playerPosition;//.position;
            activate.enabled = false;
        }
        //checks to see if the flashbang is being held in inventory

        if (inInventory == false)
        { 
            //set flash to its own transform and detach from the throw position
            flashPos = this.transform.position;
            this.transform.SetParent(null);
            this.GetComponent<Rigidbody>().useGravity = true;
            this.transform.position = flashPos;
        }
        else //(inInventory == true)
        {
            //set rigidbody values
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.transform.SetParent(throwPosition); 
            
            //if the player uses a flashbang throw the flashbang
            if (Input.GetButtonDown("Trigger"))// && player.flashbangs.Contains(flashbangCheck) && player.flashbangs.Count != 0 && flashbangCheck != null)
            {
                Debug.Log("Flashbang Thrown boy");
                if (player.flashbangs.Contains(this) && player.flashbangs.Count != 0)
                {
                    //start the fuse timer
                    startFuse = true;
                    inInventory = false;

                    //throw
                    player.flashbangs[0].GetComponent<Rigidbody>().AddForce(throwPosition.forward * throwForce);
                    //if(thrown == false)
                    //{
                    //    ThrowFlashbang(flashbangCheck,currentUse);
                    //}


                    //put audio here throwing
                    FindObjectOfType<AudioManager>().Play("Throw");



                    //resets values and destroys the used flashbang delete in the flash sounddrop script
                    temp = this;
                    player.flashbangs.Remove(this);
                    //if(currentUse.Count == 0 && thrown == false)
                    //{
                    //    currentUse.Add(this);
                    //    ThrowFlashbang(flashbangCheck, currentUse);
                    //}
                    // AssignFlashbangValues(flashbang, player.flashbangs);
                }
                else// if (player.flashbangs.Count == 0)// && flashbangCheck == null)//(Input.GetButtonDown("Trigger") && 
                {
                    //play empty audio here
                    FindObjectOfType<AudioManager>().Play("FlashlightReloadEmpty");
                }
            }
            //else if (player.flashbangs.Count == 0 && flashbangCheck == null)//(Input.GetButtonDown("Trigger") && 
            //{
            //    //play empty audio here
            //    FindObjectOfType<AudioManager>().Play("FlashlightReloadEmpty");
            //}
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (inInventory == false)
        {


            //if the player is in the radius let the player pick up the item
            if (other.gameObject.tag == "Player")
            {
                //checks to see if the player tried to pick it up
                if (player.pressedButton == true)
                {
                    if (player.flashbangs.Count < 3)
                    {
                        if (this.transform.position != playerPosition)//.position)
                        {
                            //play audio here
                            FindObjectOfType<AudioManager>().Play("BatteryPickUp");
                            AddFlashbang(this, player.flashbangs);

                            //check to state that the flashbang is in inventory
                            inInventory = true;
                            //turn off gravity while in inventory
                            this.GetComponent<Rigidbody>().useGravity = false;
                            //turn off collisions
                            this.GetComponent<Rigidbody>().detectCollisions = true;// false;

                            acquired = true;

                        }
                    }
                }
            }
        }
    }

    public void AddFlashbang(Flashbang flash, List<Flashbang> list)
    {
        if (!list.Contains(flash))
        {
            list.Add(flash);
        }
        flashbangCheck = flash;
    }

   public void AssignFlashbangValues(Flashbang flash, List<Flashbang> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            flash = list[i];
            if(flash == flashbangCheck)
            {
                if(i == 0)
                {
                    flash = list[0];
                   // temp = flash;
                   // player.flashbangs.Remove(flash);
                    Destroy(temp);
                    flashbangCheck = null;
                }
                else if(list[i-1] != null)
                {
                   // temp = flash;
                   // player.flashbangs.Remove(flash);
                    Destroy(flash);
                    if (i == 0)
                    {
                        flashbangCheck = null;
                    }
                    else
                    {
                        flashbangCheck = list[i - 1];
                    }
                }
                else
                {
                    flashbangCheck = null;
                }
            }
        }
    }

    void ThrowFlashbang(Flashbang flash, List<Flashbang> list)
    { 
        if (list.Count == 1)
        {
            list[0].GetComponent<Rigidbody>().AddForce(throwPosition.forward * throwForce);
            temp = list[0];
            list.Remove(list[0]);
           // thrown = true;
        }
        //else if(list.Count == 2)
        //{
        //    list[1].GetComponent<Rigidbody>().AddForce(throwPosition.forward * throwForce);
        //    temp = list[1];
        //    list.Remove(list[1]);
        //    thrown = true;
        //}
        //else if(list.Count == 3)
        //{
        //    list[2].GetComponent<Rigidbody>().AddForce(throwPosition.forward * throwForce);
        //    temp = list[2];
        //    list.Remove(list[2]);
        //    thrown = true;
        //}
    }

}
