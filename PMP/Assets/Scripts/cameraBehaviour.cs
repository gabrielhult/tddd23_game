using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;
    private float oldOffsetX;
    private float offsetZ;
    private float offsetX;
    public float speed;
    public bool rotateCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
        if(rotateCamera){
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, 0, offsetZ);
            rotateCamera = false;
        
        }
    }
}
