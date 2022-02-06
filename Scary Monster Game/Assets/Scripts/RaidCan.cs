using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaidCan : MonoBehaviour
{
    public PlayerBehavior player;
    public MonsterBehavior monster;
    // private List<RaidCan> raidCanInventory = new List<RaidCan>();
    private MeshRenderer visible;
    public Vector3 monsterMove;
    public GameObject minimapSprite;
    private RaidCan temp;
    public ParticleSystem Spray_Left;
    public ParticleSystem Spray_Right;
    public ParticleSystem SprayMonster;
    private float sprayTime = 1;
    public new FirstPersonView camera;
    [NonSerialized] public float canUse = 1;
    private bool deleted = false;
    [NonSerialized] public bool lookAt = false;
    public UIPickUpText text;

    // Update is called once per frame
    private void Start()
    {
        Spray_Left.Stop();
        Spray_Right.Stop();
        SprayMonster.Stop();
    }
    void Update()
    {

        if (Vector3.Distance(player.transform.position, monster.transform.position) < 4)
        {
            if (player.raidCanInventory.Count > 0)
            {
                //play the spraying animation
                SprayMonster.Play();

                monster.attacked = true;

                //transform the position of the raid can, the raid can should be at the bottom right of the screen during this time
                this.transform.position = camera.itemView[0].position;
                //triggers spray audio
                FindObjectOfType<AudioManager>().Play("Spray");
                Spray_Left.Play();
                Spray_Right.Play();
                //removes the can from inventory
                deleted = true;
                temp = player.raidCanInventory[0];
                player.raidCanInventory.Remove(player.raidCanInventory[0]);
                //display text on UI
                text.text = "Removed Raid Can...";
                text.timer = 3.0F;

            }
        }


        //continues to updates the raid cans position and turns off the raid can's shadow
        if (player.raidCanInventory.Count == 1)
        {
            visible = player.raidCanInventory[0].GetComponent<MeshRenderer>();
            player.raidCanInventory[0].transform.position = player.transform.position;
            visible.enabled = false;
        }
        else if(deleted == true)
        {
            //raid can position is the use position
            this.transform.position = camera.itemView[0].position;
            //is visible during use
            if (canUse > 0)
            {
                canUse -= Time.deltaTime;
                visible.enabled = true;
                lookAt = true;
            }
            else
            {
                visible.enabled = false;
                canUse = 1;
                deleted = false;
                lookAt = false;
                //deletes the raid
                Destroy(temp);
                this.gameObject.SetActive(false);
            }

        }
        if (sprayTime > 0 && monster.attacked == true)
        {
            sprayTime -= Time.deltaTime;
        }
        else
        {
            sprayTime = 1;
            Spray_Left.Stop();
            Spray_Right.Stop();
            SprayMonster.Stop();
        }
    }
    private void OnTriggerStay(Collider other)
    {

        //if the player is in the radius of the spray can allow the player to pick it up
        if (other.gameObject.tag == "Player")
        {
            //only allow the player to carry one raid can at a time
            if (player.raidCanInventory.Count < 1)
            {
                //checks to see if the player tried to pickup the raid can
                if (player.pressedButton == true)
                {
                    FindObjectOfType<AudioManager>().Play("RaidCanPickUp");
                    player.raidCanInventory.Add(this);
                    minimapSprite.GetComponent<SpriteRenderer>().enabled = false;
                    //display text on UI
                    text.text = "Added Raid Can...";
                    text.timer = 3.0F;
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //checks to see if this item is in the inventory
        if (player.raidCanInventory.Contains(this))
        {
            //if the player has the raid can in their inventory and the monster attacks them delete the raid can
            if (player.raidCanInventory[0].transform.position == player.transform.position)
            {
                //Debug.Log(other.gameObject.tag);
                //the monster comes into contact with the raid can collider
                if (other.gameObject.tag == "Monster")
                {



                }
                ////deletes the raid can from the players inventory 
                //temp = player.raidCanInventory[0];
                //player.raidCanInventory.Remove(player.raidCanInventory[0]);
                //Destroy(temp);
            }
           
        }
        
    }

    
}

