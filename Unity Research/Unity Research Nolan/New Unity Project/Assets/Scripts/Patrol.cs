using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Source were code was obtained - Blackthornprod - Patrol AI with Unity and C# - Easy Tutorial:
//https://www.youtube.com/watch?v=8eWbSN2T8TE&list=PLgaNWmJF3zopH7o334lf92y_ZqEd123gq&index=1
public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
