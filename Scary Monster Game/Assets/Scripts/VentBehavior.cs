using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VentBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    public Collider trigger;
    public bool isFilled;
    public ParticleSystem PoisonGas;
    private PoisonBag temp;
    // Start is called before the first frame update
    void Start()
    {
        isFilled = false;
        PoisonGas.Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player.PoisonBagInventory.Count >= 1)
            {
                if (isFilled == false)
                {
                    if (player.pressedButton)
                    {
                        isFilled = true;
                        //play audio
                        FindObjectOfType<AudioManager>().Play("VentPlacement");
                        temp = player.PoisonBagInventory[0];
                        player.PoisonBagInventory.Remove(player.PoisonBagInventory[0]);
                        Destroy(temp);
                        Debug.Log("Poison placed");
                        PoisonGas.Play();
                    }
                }
                else if (player.pressedButton)
                {
                    if (isFilled)
                    {
                        Debug.Log("Poison already inserted");
                    }
                }
            }
        }
        
    }
}
