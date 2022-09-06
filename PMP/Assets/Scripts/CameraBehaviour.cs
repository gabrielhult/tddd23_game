using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float offsetYInit;
    public float rotationSpeed;


    private float oldOffsetX;
    private float offsetZ;
    private float offsetX;
    public bool rotateCamera;

    void Start(){
        transform.position = target.position + offset;
        //transform.Rotate(0, offsetYInit, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
        if(rotateCamera){
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, offset.y, offsetZ);
            //Smooth rotation dream..=? <3
            //Quaternion toRotation = Quaternion.LookRotation(offset, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            transform.Rotate(0, -90, 0);
            rotateCamera = false;
        
        }
    }
}
