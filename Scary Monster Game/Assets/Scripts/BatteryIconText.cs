using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryIconText : MonoBehaviour
{
    public PlayerBehavior player;
    private Text display;
   // [NonSerialized] public string text;
    // Start is called before the first frame update
    void Start()
    {
        display = this.GetComponent<Text>();
        display.text = player.batteries.Count + " / 3";
    }

    // Update is called once per frame
    void Update()
    {
        display.text = player.batteries.Count + " / 3";
    }
}
