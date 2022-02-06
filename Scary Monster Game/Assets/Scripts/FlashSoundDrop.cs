using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSoundDrop : MonoBehaviour
{
    public float fuseTime = 3;
    public float lifespan = 2;
    private SphereCollider soundDrop;
    public Flashbang flashbang;
    public PlayerBehavior player;
    public new AudioSource audio;
    private bool playSound = false;
    public ParticleSystem visual;
    //Flashbang temp;
    // Start is called before the first frame update
    void Start()
    {
        soundDrop = this.GetComponent<SphereCollider>();
        soundDrop.enabled = false;
        //flashbang = flashbang.flashbangCheck;
        visual.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //start the sound drop after the timer ends
        if(flashbang.startFuse == true)
        {
           // flashbang.thrown = false;
            if(fuseTime > 0)
            {
                fuseTime -= Time.deltaTime;
            }
            else
            {
                playSound = true;
                //play detenation audio here
                if(playSound == true)
                {
                    if(audio.isPlaying == false)
                    {
                        audio.Play();
                        visual.Play();
                    }
                }
                //once the timer is done reset the values and activate the sound drop and effects
                if (lifespan > 0)
                {
                    soundDrop.enabled = true;
                    lifespan -= Time.deltaTime;
                }
                else
                {
                    soundDrop.enabled = false;
                    flashbang.AssignFlashbangValues(flashbang, player.flashbangs);
                    flashbang.startFuse = false;
                    playSound = false;
                    audio.Stop();
                    visual.Stop();
                }

            }
        }
    }
}
