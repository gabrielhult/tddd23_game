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
            //Debug.Log("Goal!");
            GameManager.Instance.NextLevel(); //Inför nån form av Invoke så det blir liite delayed?
        }else if(other.tag == "Banana"){
            GameManager.Instance.CollectBanana();
        }else if(other.tag == "Hazard"){
            Debug.Log("DIE");
            GameManager.Instance.isGameOver = true;
        }
    }

    void OnCollisionEnter(Collider other) {
        //Debug.Log(other.collider.name);
        if(other.tag == "Ground"){
            //Debug.Log("Ground!");
        }
    }
}
