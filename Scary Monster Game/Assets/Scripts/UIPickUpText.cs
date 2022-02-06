using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickUpText : MonoBehaviour
{
    public PlayerBehavior player;
    private Text display;
    [NonSerialized] public string text;
    [NonSerialized] public float timer;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        display = this.GetComponent<Text>();
        color = display.color;
        color.a = 0F;
    }

    // Update is called once per frame
    void Update()
    {
        display.text = text;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            display.CrossFadeAlpha(1.0F, 0.05F, false);
        }
        else
        {
            display.CrossFadeAlpha(0.0F, 0.05F, false);
        }
    }
}
