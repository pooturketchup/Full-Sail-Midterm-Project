using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapWallBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GetComponentInParent<Transform>().position;
    }
}
