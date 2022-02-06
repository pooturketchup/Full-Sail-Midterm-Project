using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentTextBehavior : MonoBehaviour
{

    public Text txt;
    public int ventsRemaining = 0;
    public GameObject vent1;
    public GameObject vent2;
    public GameObject vent3;
    public GameObject vent4;
    public GameObject vent5;
    public GameObject vent6;
    public GameObject vent7;
    public GameObject vent8;

    private string scream;

    bool vent1Filled;
    bool vent2Filled;
    bool vent3Filled;
    bool vent4Filled;
    bool vent5Filled;
    bool vent6Filled;
    bool vent7Filled;
    bool vent8Filled;



    // Start is called before the first frame update
    void Start()
    {
        vent1Filled = false;
        vent2Filled = false;
        vent3Filled = false;
        vent4Filled = false;
        vent5Filled = false;
        vent6Filled = false;
        vent7Filled = false;
        vent8Filled = false;

        scream = "MonsterPoisonedScream";
    }

    // Update is called once per frame
    void Update()
    {
        //changes the monster scream audio for the last vent
        if(ventsRemaining == 1)
        {
            scream = "MonsterDeath";
        }

        if (vent1.GetComponent<VentBehavior>().isFilled && vent1Filled == false)
        {
            ventsRemaining++;
            vent1Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent2.GetComponent<VentBehavior>().isFilled && vent2Filled == false)
        {
            ventsRemaining++;
            vent2Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent3.GetComponent<VentBehavior>().isFilled && vent3Filled == false)
        {
            ventsRemaining++;
            vent3Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent4.GetComponent<VentBehavior>().isFilled && vent4Filled == false)
        {
            ventsRemaining++;
            vent4Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent5.GetComponent<VentBehavior>().isFilled && vent5Filled == false)
        {
            ventsRemaining++;
            vent5Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent6.GetComponent<VentBehavior>().isFilled && vent6Filled == false)
        {
            ventsRemaining++;
            vent6Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent7.GetComponent<VentBehavior>().isFilled && vent7Filled == false)
        {
            ventsRemaining++;
            vent7Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }
        if (vent8.GetComponent<VentBehavior>().isFilled && vent8Filled == false)
        {
            ventsRemaining++;
            vent8Filled = true;
            FindObjectOfType<AudioManager>().Play(scream);
        }

        txt.text = ventsRemaining.ToString();
    }
}
