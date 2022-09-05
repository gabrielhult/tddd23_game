using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private string collidedObjectTag;
    //gives tag of object collided with, return this value to GameManager to decide what happens then

    void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger");
        if(other.tag == "Goal"){
            //Debug.Log("Wait!");
            GameManager.Instance.NextLevel(); //Inför nån form av Invoke så det blir liite delayed?
        }
    }
}
