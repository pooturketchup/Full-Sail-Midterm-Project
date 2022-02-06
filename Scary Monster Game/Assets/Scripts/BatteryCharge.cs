using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryCharge : MonoBehaviour
{
    private Image progress;
    [NonSerialized] public float charge = 1F;
    public float timer = 180;
    private float resetTimer;
    [NonSerialized] public bool reset = false;
    public Flashlight flashlight;
    // Start is called before the first frame update
    void Start()
    {
        progress = this.GetComponent<Image>();
        progress.fillAmount = charge;
        resetTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashlight.flashlight.enabled == true)
        {
            progress.fillAmount = charge;
            charge -= 0.000034F;
        }

        if(reset == true)
        {
            timer = resetTimer;
            charge = 1F;
            reset = false;
        }
    }
}
