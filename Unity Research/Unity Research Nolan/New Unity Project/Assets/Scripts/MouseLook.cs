using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Source were code was obtained - Brackeys - First Person Movement in Unity- FPS Controller: 
//https://www.youtube.com/watch?v=_QajrabyTJc&list=PLgaNWmJF3zopH7o334lf92y_ZqEd123gq&index=2
public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform playerbody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerbody.Rotate(Vector3.up * mouseX);
    }
}
