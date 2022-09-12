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
        //GetComponent<Rigidbody>().velocity = new Vector3(3,0,0); //Kanske kör på detta istället?
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
        if(rotateCamera){ //Kan vara orelevant del nu!
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, offset.y, offsetZ);
            transform.Rotate(0, -90, 0);
            rotateCamera = false;
        
        }
    }
}
