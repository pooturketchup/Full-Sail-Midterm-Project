using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;

public class SoundDrop : MonoBehaviour
{
    
    public GameObject source;
   // private float lifeSpan; //may have to change this to a timer
    private float radius;
    private Vector3 drop;//adjust drop position to be the lower end of the player
    // Start is called before the first frame update
    void Start()
    {
        radius = this.GetComponent<SphereCollider>().radius;
        radius = 0F;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    //checks to see if collision needs to be ignored only thing it should collide with is the monster listener
    //    if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Player")
    //    {
    //        Physics.IgnoreCollision(collision.collider, this.GetComponent<SphereCollider>());
    //    }
    //}

   

    // Update is called once per frame
    void Update()
    {
        //adjust lifespan and radius for crouching
        if (Input.GetButton("Crouch"))
        {
            // lifeSpan = 1.5F * Time.deltaTime;
            // radius = 3F;

            //when the player is crouching the sound drop radius can decrease in size only goes to 0 if the player is not moving
           if (radius > 2)
            {
              radius -= Time.deltaTime*2;
            }
           
            //if the player isn't moving the sound drop may decrease to 0 (maybe check to see if wasd buttons are held down?)
            if(radius < 2)
            {
                radius += Time.deltaTime*2;
            }

            //if player isn't moving set radius to 0
            if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
            {
                if (radius > .05f)
                {
                    radius -= .03f;
                }
            }

            drop = new Vector3(source.transform.position.x, source.transform.position.y - 1, source.transform.position.z);
        }
        //adjust lifespan and radius for running
        else if (Input.GetButton("Run"))
        {
           // lifeSpan = 5 * Time.deltaTime;
           // radius = 10;

            //when the player is running increase the radius
            if (radius < 15)
            {
                radius += Time.deltaTime*3;
            }
            //setting maximum size
            if (radius > 15)
            {
                radius -= Time.deltaTime*2;
            }

            //if player isn't moving set radius to 0
            if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
            {
                if (radius > .05f)
                {
                    radius -= .03f;
                }
            }

            drop = new Vector3(source.transform.position.x, source.transform.position.y - 1, source.transform.position.z);
        }
        //adjust lifespan and radius for walking
        else 
        {
            //radius = 5;
           // lifeSpan = 3 * Time.deltaTime;

            //if the radius is under the standard size increment
            if(radius < 5)
            {
                radius += Time.deltaTime * 2;
            }
            //if the radius is over the standard size decrement
            else if (radius > 5)
            {
                radius -= Time.deltaTime * 2;
            }
            //once radius is set to standard keep it there until movement changes
            else
            {
                radius = 5;
            }

            //if player isn't moving set radius to 0
            if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
            {
                if (radius > .05f)
                {
                    radius -= .03f;
                }
            }

            drop = new Vector3(source.transform.position.x, source.transform.position.y - 1, source.transform.position.z);
        }

        //creation of the sound drop setting of the scale and position

        //setting the scale of the sound drop collider
        this.GetComponent<SphereCollider>().radius = radius;
        //adjust the position of the sound drop to the correct location
        this.transform.position = drop;

        //after lifespand is over delete the sound drop
        //lifeSpan -= Time.deltaTime;
        //if(lifeSpan == 0)
        //{
        //    Destroy(this.gameObject);
        //}
        //for (float i = 0; i < lifeSpan; i++)
        //{
        //    if (i == lifeSpan)
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    
    //private Vector3 DropLocation(Vector3 drop, float lifeSpan)
    //{
        
    //}
}
