using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PickUpText : MonoBehaviour
{
   public GameObject source;
    TextMesh txt;
    private MeshRenderer txtDisplay;
    private MeshRenderer visible;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //setup text
        txt = GetComponent<TextMesh>();
        txtDisplay = GetComponent<MeshRenderer>();
        txtDisplay.enabled = false;

        //check source meshrender
        visible = source.GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            txtDisplay.enabled = true;
        }
        else
        {
            txtDisplay.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            txtDisplay.enabled = false;
        }
    }

    private void Update()
    {
        //text always looks at the player
        transform.LookAt(player);

        if(visible.enabled == false)
        {
            txtDisplay.enabled = false;
        }
    }
}

public static class Vector3Extensions
{
    public static Vector3 Inverse(this Vector3 v)
    {
        return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
    }
}
