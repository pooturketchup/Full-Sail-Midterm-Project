using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//Source were code was obtained - Brackeys - First Person Movement in Unity- FPS Controller:
//https://www.youtube.com/watch?v=_QajrabyTJc&list=PLgaNWmJF3zopH7o334lf92y_ZqEd123gq&index=2
public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5F;
    public float gravity = 20F;
    private Vector3 moveDirection = Vector3.zero;
    public List<PoisonBag> PoisonBagInventory = new List<PoisonBag>();
    [NonSerialized] public bool pressedButton = false;
    [NonSerialized] public bool placeDown = false;
    public List<RaidCan> raidCanInventory = new List<RaidCan>();
    [NonSerialized] public bool playerMovement;
    private CharacterController controller;
    public List<Key> keys = new List<Key>();
    public List<ToySound> toys = new List<ToySound>();
    public List<Battery> batteries = new List<Battery>();
    public List<Flashbang> flashbangs = new List<Flashbang>();
    public GameObject cameraDisplay;
    public Camera Security1;
    public Camera Security2;
    public Camera Security3;
    public Camera Security4;
    bool cameraToggle = false;
    [NonSerialized] public bool hide = false;
    private bool playSound = false;
    public new AudioSource audio;
    private void Start()
    {
        playerMovement = true;
        controller = GetComponent<CharacterController>();
        Security2.enabled = false;
        Security3.enabled = false;
        Security4.enabled = false;

    }
    //Player movement
    private void Update()
    {
        if (playerMovement)
        {
            if (controller.isGrounded)
            {
                float delta = Input.GetAxis("Mouse ScrollWheel");
                // Debug.Log(delta);
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.GetButton("Crouch"))
                {
                    speed = 2.5F;
                }
                else if (Input.GetButton("Run"))
                {
                    //makes the player breath heavily after they run
                   // FindObjectOfType<AudioManager>().Play("HeavyBreathing");
                    speed = 10F;
                }
                else if (speed != 5F)
                {
                    speed = 5F;
                }
                if(speed == 10 && controller.velocity != Vector3.zero)
                {
                    playSound = true;
                    if(playSound == true)
                    {
                        if (audio.isPlaying == false)
                        {
                            audio.Play();
                        }
                    }
                }

                if (Input.GetButton("PickUp"))
                {
                    pressedButton = true;
                }
                else
                {
                    pressedButton = false;
                }
                if (Input.GetButton("PlaceDown"))
                {
                    placeDown = true;
                }
                else
                {
                    placeDown = false;
                }
                if (Input.GetButton("Trigger"))
                {
                    hide = true;
                }
                else
                {
                    hide = false;
                }
                if (Input.GetButtonDown("Camera"))
                {
                    cameraToggle = !cameraToggle;
                    if (cameraToggle == true)
                    {
                        cameraDisplay.SetActive(true);
                    }
                    else
                    {
                        cameraDisplay.SetActive(false);
                    }
                }
                if(cameraToggle == true)
                {
                    if (delta == 0.1f)
                    {
                        if (Security1.enabled == true)
                        {
                            Security1.enabled = false;
                            Security4.enabled = true;
                        }
                        else if (Security2.enabled == true)
                        {
                            Security2.enabled = false;
                            Security1.enabled = true;
                        }
                        else if (Security3.enabled == true)
                        {
                            Security3.enabled = false;
                            Security2.enabled = true;
                        }
                        else if (Security4.enabled == true)
                        {
                            Security4.enabled = false;
                            Security3.enabled = true;
                        }
                    }
                    else if (delta == -0.1f)
                    {
                        if (Security1.enabled == true)
                        {
                            Security1.enabled = false;
                            Security2.enabled = true;
                        }
                        else if (Security2.enabled == true)
                        {
                            Security2.enabled = false;
                            Security3.enabled = true;
                        }
                        else if (Security3.enabled == true)
                        {
                            Security3.enabled = false;
                            Security4.enabled = true;
                        }
                        else if (Security4.enabled == true)
                        {
                            Security4.enabled = false;
                            Security1.enabled = true;
                        }
                    }
                }

            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

