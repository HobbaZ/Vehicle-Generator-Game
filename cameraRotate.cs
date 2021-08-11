using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotate : MonoBehaviour
{
    public Vector3 target;
    public float rotateSpeed = 40.0f;

    public float maxHeight;
    public float minHeight;
    //public float maxZoom;
    //public float minZoom;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        //Keyboard controls
        //Zoom camera
        //zoom in
        if((Input.GetKey(KeyCode.Z)) || (Input.GetKey(KeyCode.Alpha1))) {
            transform.Translate(Vector3.forward * 10.0f *Time.deltaTime, Space.Self);
        }
        // zoom out
         else if((Input.GetKey(KeyCode.X)) || (Input.GetKey(KeyCode.Alpha2))) {
            transform.Translate(Vector3.forward * -10.0f *Time.deltaTime, Space.Self);
        }

        //Rotate camera
        //left
        if((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow))) {
            transform.Translate(Vector3.right * -rotateSpeed *Time.deltaTime, Space.Self);
        //transform.RotateAround(target, Vector3.up, rotateSpeed*Time.deltaTime);
    }

        //right
        else if((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow))) {
            transform.Translate(Vector3.right * rotateSpeed *Time.deltaTime, Space.Self);
        //transform.RotateAround(target, Vector3.up, -rotateSpeed*Time.deltaTime);
    }

        //up
        else if((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow))) {
            transform.Translate(Vector3.up * rotateSpeed *Time.deltaTime, Space.Self);
        //transform.RotateAround(target, Vector3.right, rotateSpeed*Time.deltaTime);
    }

        //down
        else if((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow))) {
            transform.Translate(Vector3.up * -rotateSpeed *Time.deltaTime, Space.Self);
        //transform.RotateAround(target, Vector3.right, -rotateSpeed*Time.deltaTime);
    }

        //Initial position
        Vector3 camPosY = transform.position;

        //height clamp
        //camPosY.y = Mathf.Clamp(camPosY.y, minHeight, maxHeight);
        //transform.position = camPosY;

        //Zooming clamp
        //camPosZ.x = Mathf.Clamp(camPosZ.x, minZoom, maxZoom);
        //transform.position = camPosZ;
}
}
