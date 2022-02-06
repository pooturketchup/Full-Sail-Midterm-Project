using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private GameObject lightObject;
    [NonSerialized] public Light flashlight;
    [NonSerialized] public Battery battery;
    [NonSerialized] public Battery batteryCheck;
    private Battery temp;
    public PlayerBehavior player;
    public float chargerTime = 180;
    private float resetTimer;
    public UIPickUpText text;
    public BatteryCharge charge;
    // Start is called before the first frame update
    void Start()
    {
        lightObject = GameObject.Find("Flashlight");
        flashlight = lightObject.GetComponent<Light>();
        resetTimer = this.GetComponent<Flashlight>().chargerTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            flashlight.enabled = !flashlight.enabled;
            //play audio here
            FindObjectOfType<AudioManager>().Play("FlashlightToggle");
        }

        //as time passes by the charge will decrease if the flashlight is on
        if(chargerTime > 0 && flashlight.enabled == true)
        {
            chargerTime -= Time.deltaTime;
            flashlight.intensity -= 0.00005F;
            
            if(flashlight.intensity < 0.5F)
            {
                flashlight.intensity = 0.5F;
            }
        }
        //if the player uses a battery recharge the flashlight
        if (Input.GetButtonDown("Reload") && player.batteries.Contains(batteryCheck) && player.batteries.Count != 0 && batteryCheck != null)
        {
            //resets values and destroys the used battery
            AssignBatteryValues(battery, player.batteries);
            //put audio here
            FindObjectOfType<AudioManager>().Play("FlashlightReload");

            chargerTime = resetTimer;//180;
            flashlight.intensity = 2;

            //let the player know the used a battery
            text.text = "Removed Battery...";
            text.timer = 3.0F;

            //reset the batteryCharge
            charge.reset = true;
        }
        else if(Input.GetButtonDown("Reload") && player.batteries.Count == 0 && batteryCheck == null)
        {
            //play audio here
            FindObjectOfType<AudioManager>().Play("FlashlightReloadEmpty");
        }


    }

    //this function assigns the value of the batteries so that the player can recharge the flash light with a battery
    void AssignBatteryValues(Battery bat, List<Battery> list)
    {
        Debug.Log("batteries left " + player.batteries.Count);
        for(int i = 0; i < list.Count; i++)
        {
            Debug.Log("I Value = " + i);
            bat = list[i];
            if(bat == batteryCheck)
            {
                if (i == 0) 
                {
                    bat = list[0];
                    temp = bat;
                    player.batteries.Remove(bat);
                    Destroy(temp);
                    batteryCheck = null;
                }
                else if(list[i-1] != null)
                {
                    temp = bat;
                    player.batteries.Remove(bat);
                    Destroy(temp);
                    if(i == 0)
                    {
                        batteryCheck = null;
                    }
                    else
                    {
                        batteryCheck = list[i - 1];
                    }
                }
                else
                {
                    batteryCheck = null;
                }
            }
        }
    }
}
