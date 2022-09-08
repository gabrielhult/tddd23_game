using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private string collidedObjectTag;
    //gives tag of object collided with, return this value to GameManager to decide what happens then


    void OnTriggerEnter(Collider other) {
        if(other.tag == "Goal"){
            //Debug.Log("Goal!");
            GameManager.Instance.NextLevel(); //Inför nån form av Invoke så det blir liite delayed?
        }else if(other.tag == "Banana"){
            GameManager.Instance.CollectScore(other);
        }else if(other.tag == "Hazard"){
            GameManager.Instance.isGameOver = true;
        }else if(other.tag == "Bamboo"){    
            GameManager.Instance.isClimbable = true;
            GameManager.Instance.setClimbObject(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Bamboo"){
            //Debug.Log("You can't climb anymore");
            GameManager.Instance.isClimbable = false;
        }
    }

}
