using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBarBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    float speed;
    public RectTransform rectPosition;
    float barPosition = 750;
    public CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        // Get player's speed
        speed = player.speed;

        if(barPosition == 790 && playerController.velocity == Vector3.zero)
        {
            
        }
        else if (playerController.velocity == Vector3.zero && rectPosition.transform.position.x >= 790)
        {
            barPosition -= 1;
            rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);
        }
        else
        {
            if (speed == 2.5)
            {
                if (barPosition < 850)
                {
                    barPosition += 1;
                    rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);

                }

                if (barPosition > 850)
                {
                    barPosition -= 1;
                    rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);

                }
            }


            if (speed == 5)
            {
                if (barPosition < 950)
                {
                    barPosition += 1;
                    rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);

                }

                if (barPosition > 950)
                {
                    barPosition -= 1;
                    rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);

                }
            }


            if (speed == 10)
            {
                if (barPosition < 1100)
                {
                    barPosition += 1;
                    rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);

                }

                if (barPosition > 1100)
                {
                    barPosition -= 1;
                    rectPosition.transform.position = new Vector3(barPosition, transform.position.y, transform.position.z);

                }
            }
        }
    }
}
