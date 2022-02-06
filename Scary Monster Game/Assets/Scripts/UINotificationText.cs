using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UINotificationText : MonoBehaviour
{
    public PlayerBehavior player;
    private Text display;
    [NonSerialized] public string text;
    [NonSerialized] public float timer;
    [NonSerialized] public bool show = false;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        display = this.GetComponent<Text>();
        display.enabled = false;
        color = display.color;
    }

    // Update is called once per frame
    void Update()
    {
        display.text = text;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            display.enabled = true;
        }
        else
        {
            display.enabled = false;
        }

        //if (show == true)
        //{
        //    display.enabled = true;
        //}
        //else
        //{
        //    display.enabled = false;
        //}
    }
}
