using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timer = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer for duration fo death scene
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            //reload the main game
            SceneManager.LoadScene("Mansion");
        }
    }
}
