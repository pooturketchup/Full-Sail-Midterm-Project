using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBag : MonoBehaviour
{
    public PlayerBehavior player;
    private MeshRenderer visible;
    public SpriteRenderer minimapSprite;
    public UIPickUpText text;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //continues to updates the bags position and turns off the bags shadow
        if (player.PoisonBagInventory.Count == 1)
        {
            visible = player.PoisonBagInventory[0].GetComponent<MeshRenderer>();
            player.PoisonBagInventory[0].transform.position = player.transform.position;
            visible.enabled = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        //if the player is in the radius of the bag can allow the player to pick it up
        if (other.gameObject.tag == "Player")
        {
            //only allow the player to carry one bag at a time
            if (player.PoisonBagInventory.Count < 1)
            {
                if (this.transform.position != player.transform.position)
                {
                    //checks to see if the player tried to pickup the bag
                    if (player.pressedButton == true)
                    {
                        FindObjectOfType<AudioManager>().Play("BagPickUp");
                        player.PoisonBagInventory.Add(this);
                        minimapSprite.enabled = false;
                        Debug.Log("Added");
                        //display text on UI
                        text.text = "Added Poison Bag...";
                        text.timer = 3.0F;
                        this.GetComponent<SphereCollider>().enabled = false;
                    }
                }
            }
        }
    }
}
