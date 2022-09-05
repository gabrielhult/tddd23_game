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
        if(GameManager.Instance.levelCount % 4 == 0 && rotateCamera == true){
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, 0, offsetZ);
            rotateCamera = false;
        }else if(GameManager.Instance.levelCount % 4 == 1 && rotateCamera == true){
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, 0, offsetZ);
            rotateCamera = false;
        }else if(GameManager.Instance.levelCount % 4 == 2 && rotateCamera == true){
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, 0, offsetZ);
            rotateCamera = false;
        }else if(GameManager.Instance.levelCount % 4 == 3 && rotateCamera == true){
            oldOffsetX = offset.x;
            offsetX = offset.z * -1;
            offsetZ = oldOffsetX;
            offset = new Vector3(offsetX, 0, offsetZ);
            rotateCamera = false;
        }
        Debug.Log("OffsetX: " +  offsetX);
        Debug.Log("OffsetZ: " +  offsetZ);
    }
}
