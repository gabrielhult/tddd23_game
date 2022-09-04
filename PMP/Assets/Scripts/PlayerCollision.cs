using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public string collidedObjectTag;
    //gives tag of object collided with, return this value to GameManager to decide what happens then
    void OnCollisionEnter(Collision other) {
        collidedObjectTag = other.collider.name;
        Debug.Log(other.collider.name);
    }
}
