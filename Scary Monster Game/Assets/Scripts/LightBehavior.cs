using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    private float timer;
    private float resetTimer;
    private new Light light;
    public float coolDown;
    private float resetCoolDown;
    // Start is called before the first frame update
    void Start()
    {
        light = this.GetComponent<Light>();
        timer = Random.Range(60,120);
        resetTimer = timer;
        resetCoolDown = this.GetComponent<LightBehavior>().coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            //turn off the lights for a set amount of time
            if(coolDown > 0)
            {
                coolDown -= Time.deltaTime;
                light.enabled = false;
            }
            else
            {
                coolDown = resetCoolDown;
                timer = resetTimer;
                light.enabled = true;
            }
        }
    }
}
