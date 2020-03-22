using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float yawSpeed; // = to the player's rotate speed !!!
    //public float yawInput = 0f;
    // not used for the moment, but it might cause problems when the player can't rotate and the camera does

    private float currentYaw = 0f;
    private float currentZoom = 10f;

    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentYaw += Input.GetAxis("Mouse X")*yawSpeed;
    }


    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        //place the camera on the right place from the center of the player
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        //turn at the right time
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
