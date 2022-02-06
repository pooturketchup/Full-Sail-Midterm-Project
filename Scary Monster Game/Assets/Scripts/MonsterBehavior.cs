using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class MonsterBehavior : MonoBehaviour
{
    Animator anim;
    public Vector3 target;
    public NavMeshAgent agent;
    public float walkRadius;
    NavMeshHit next;
    public GameObject minionSpawn;
    public RaidCan raid;
    public bool attacked;
    public PlayerBehavior player;
    public CharacterController playerController;
    public GameObject soundDrop;
    public VentTextBehavior vents;
    public new FirstPersonView camera;
    public float isFound;
    private bool scream;
    private int audiotrigger;
    private float timer = 2;
    public ParticleSystem poison1;
    public ParticleSystem poison2;
    public ParticleSystem poison3;
    public VentBehavior[] vent;
    public Vector3[] roomPoints;
    float raidTimer = 10;
    float walkTimer = 60;
    //int rand0to4 = UnityEngine.Random.Range(0, 4);
    bool canBoost = false;
    public Transform[] CameraMoveSpot;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = player.GetComponent<CharacterController>();
        target = Idle();
        isFound = 0;
        scream = false;
        audiotrigger = 0;
        poison1.Stop();
        poison2.Stop();
        poison3.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        //set value of raidcan
        if (player.raidCanInventory.Count == 1)
        {
            raid = player.raidCanInventory[0];
        }

        // Set walk/run animation
        anim.SetFloat("speed", agent.velocity.normalized.magnitude);

        // Raycast from monster to player
        Vector3 ray = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (Physics.Raycast(ray, player.transform.position, Mathf.Infinity))
        {
            Debug.DrawRay(ray, player.transform.position - ray);
        }

        // 90 degree field of view to see the player in
        Vector3 playerDirection = player.transform.position - transform.position;
        float angle = Vector3.Angle(playerDirection, transform.forward);

        #region Movement towards a target

        // If not hit by raid can
        if (attacked == false)
        {
            // If distance between monster and player is less than 10 (how far away the monster can hear)
            if (Vector3.Distance(transform.position, player.transform.position) <= 20 && Physics.Raycast(ray, player.transform.position, Mathf.Infinity))
            {
                // Vent-dependent speed boost
                if (canBoost)
                {
                    agent.speed = 15;
                }

                // Chase the player
                target = player.transform.position;
            }
            // If distance between monster and soundDrop is less than 10
            else if (Vector3.Distance(transform.position, soundDrop.transform.position) <= 20)
            {
                agent.speed = 10;

                // Chase the soundDrop
                target = soundDrop.transform.position;
            }
            else
            {
                if (target != player.transform.position && target != soundDrop.transform.position)
                {
                    agent.speed = 5;

                    if (agent.remainingDistance < 2)
                    {
                        target = Idle();
                    }
                }
                else
                {
                    target = Idle();
                }
            }
        }

        // Actually moves monster to its current target
        //  Debug.Log("target is " + target);
        agent.SetDestination(target);

        #endregion

        #region Attacked by Raid

        // If hit by raid can
        if (attacked == true)
        {
            if (raid.lookAt == true)
            {
                //make the player look at the monster while the can is being sprayed
                camera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.75F, this.transform.position.z));
            }

            soundDrop.SetActive(false);
            agent.speed = 10;

            // run away from player
            Vector3 topFloor = new Vector3(-8, 8, -22);
            Vector3 bottomFloor = new Vector3(-17, 1, -33);

            if (transform.position.y < 5)
            {
                target = topFloor;
            }
            else if (transform.position.y > 5)
            {
                target = bottomFloor;
            }

            // Wait until monster can attack the player again
            if (raidTimer > 0)
            {
                raidTimer -= Time.deltaTime;
            }
            else
            {
                agent.speed = 5;
                soundDrop.SetActive(true);
                raidTimer = 10;
                attacked = false;
            }
        }

        #endregion

        #region Vent-dependent Behaviors

        if (vents.ventsRemaining <= 2)
        {
            // Bool condition for speed boost when player is target
            canBoost = true;

            if (vents.ventsRemaining <= 4) // aggro speed boost
            {
                if (CompareTag("Minion"))
                {
                    transform.position = minionSpawn.transform.position;
                }

            }
        }

        #endregion

        #region Monster Killed

        // If the number of vents remaining = 0, kill monster
        if (vents.ventsRemaining == 8)
        {
            //Turn poison gas on
            poison1.Play();
            poison2.Play();
            poison3.Play();

            ////Lerp the camera to the monster position should be right in front of the monster's face
            //MonsterPosition = (new Vector3(this.transform.position.x, this.transform.position.y + 1.75F, this.transform.position.z - 4F));

            //camera.transform.position = Vector3.Lerp(camera.transform.position, MonsterPosition, 1);
            ////Make the camera face the monster
            //camera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.75F, this.transform.position.z));

            //Lerp the camera to the movespot in front of the monster
            camera.transform.position = Vector3.Lerp(camera.transform.position, CameraMoveSpot[1].position, 1);
            //Make the camera face the monster
            camera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.75F, this.transform.position.z));
            agent.SetDestination(transform.position);
            anim.SetBool("dead", true);

            //trigger the monster scream audio
            scream = true;
            if (scream == true && audiotrigger == 0)
            {
                audiotrigger++;
            }
            //start the timer here once the timer ends reload the scene
            if (audiotrigger == 1 && timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                //reload the main game
                SceneManager.LoadScene("Main Menu");//used to be Mansion
            }

        }

        #endregion

        #region Player Killed

        // If player doesn't have a raid can and...
        if (attacked == false)
        {
            // If the monster is targeting the player and is close enough and...

            if (Vector3.Distance(transform.position, player.transform.position) < 4F)
            {
                    if (player.raidCanInventory.Count < 1)
                    {
                        //player can no longer move
                        camera.playerRotation = false;
                        player.playerMovement = false;
                        playerController.enabled = false;

                        //monster will no longer move
                        agent.enabled = false;

                        //Lerp the camera to the movespot in front of the monster
                        camera.transform.position = Vector3.Lerp(camera.transform.position, CameraMoveSpot[0].position, 1);

                        //Make the camera face the monster
                        camera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.75F, this.transform.position.z));

                        // //calulate the values of x and z axis for the lerping of the camera
                        // //if(this.transform.position.x > player.transform.position.x && this.transform.position.z > player.transform.position.z)
                        // //{
                        // //    camH = 1.25F;
                        // //}
                        //  zpos = (float)Math.Cos(90) * 1.25F;
                        //  float xsqrt = (float)(Math.Pow(1.25F,2) - Math.Pow(zpos, 2));
                        //  xpos = (float)Math.Sqrt(xsqrt);
                        //// float angleRatio = (this.transform.position.x - player.transform.position.x) / 2;
                        //// float u = angleRatio * 1.25F;

                        // //Lerp the camera to the monster position should be right in front of the monster's face
                        // // MonsterPosition = (new Vector3(this.transform.position.x + 1.25F, this.transform.position.y + 1.75F, this.transform.position.z));// - 1.25F));
                        // MonsterPosition = (new Vector3(this.transform.position.x + xpos, this.transform.position.y + 1.75F, this.transform.position.z + zpos));

                        // camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(MonsterPosition.x, MonsterPosition.y, MonsterPosition.z), 1);
                        // //Make the camera face the monster
                        // camera.transform.LookAt(new Vector3(this.transform.position.x, this.transform.position.y + 1.75F, this.transform.position.z));

                        //trigger the monster scream audio
                        if (audiotrigger == 0)
                        {
                            audiotrigger++;
                            FindObjectOfType<AudioManager>().Play("PlayerDeath");
                        }
                        //start the timer here once the timer ends reload the scene
                        if (audiotrigger == 1 && timer > 0)
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
        }

        #endregion

    }

    // Makes the monster move randomly when player hasnt been found
    public Vector3 Idle()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out next, walkRadius, 1);
        Vector3 finalPosition = next.position;
        return finalPosition;
    }
}