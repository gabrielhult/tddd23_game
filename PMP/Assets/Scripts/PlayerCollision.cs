using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private string collidedObjectTag;
    //gives tag of object collided with, return this value to GameManager to decide what happens then

    void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Goal"){
            //GameManager.NextLevel(); Fixa detta error
        }
        //Debug.Log(collidedObjectTag);
    }
}
