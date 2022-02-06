using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidImage : MonoBehaviour
{
    public PlayerBehavior player;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //turns the image on or off if the player has that item in their inventory
        if(player.raidCanInventory.Count == 1)
        {
            img.enabled = true;
        }
        else
        {
            img.enabled = false;
        }
    }
}
