using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandFatherClock : MonoBehaviour
{
    public float timer;
    private float resetTimer;
    private float soundDuration = 10;
    public GameObject source;
    private float radius;
    private bool playSound = false;
    public new AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<SphereCollider>().radius;
        resetTimer = this.GetComponentInChildren<GrandFatherClock>().timer;
    }

    // Update is called once per frame
    void Update()
    {
        //sets a timer for the clock to go off every time the timer is done
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if(soundDuration == 10)
            playSound = true;

            //play audio here
            if(playSound == true)
            {
                if(audio.isPlaying == false)
                {
                    audio.Play();
                }
            }
            //has the sound drop increase for a set duration then resets the timer
            if(soundDuration > 0)
            {
                radius += 5 * Time.deltaTime;
                soundDuration -= Time.deltaTime;
            }
            else
            {
                //reset values
                timer = resetTimer;// 300;
                soundDuration = 10;
                playSound = false;
                audio.Stop();
            }
        }
        //shrinks the size of the sound drop when clock is not ringing
        if (soundDuration == 10)
        {
            radius -= 5 * Time.deltaTime;
            if (radius < 0.5)
            {
                radius = 0.5F;
            }
        }
        this.GetComponent<SphereCollider>().radius = radius;
    }
}
