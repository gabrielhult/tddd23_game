using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private string collidedObjectTag;
    //gives tag of object collided with, return this value to GameManager to decide what happens then


    void OnTriggerEnter(Collider other) {
        if(other.tag == "Banana"){
            GameManager.Instance.CollectBanana(other.gameObject);
        }else if(other.tag == "Hazard" || other.tag == "Lava"){
            GameManager.Instance.GameOver();
        }else if(other.tag == "Bamboo"){    
            GameManager.Instance.isClimbable = true;
            GameManager.Instance.setClimbObject(other.gameObject);
        }else if(other.tag == "Sideways" || other.tag == "UpAndDown"){
            GameManager.Instance.GameOver();
        }else if(other.tag == "DefaultTile"){ //TODO: Kolla om pitchen kan justeras, annars ha helt olika instanser av samma ljud men olika pitch
            //Debug.Log("OKEJDEF");
            AudioManager.Instance.DefaultPitch("GameMusic");
        }else if(other.tag == "ArcticTile"){
            //Debug.Log("OKEJARC");
            GameManager.Instance.isArctic = true;
            AudioManager.Instance.ArcticPitch("GameMusic");
        }else if(other.tag == "MagmaTile"){
            //Debug.Log("OKEJMAG");
            AudioManager.Instance.MagmaPitch("GameMusic");
        }else if(other.tag == "SwampTile"){
            GameManager.Instance.isSwamp = true;
            //Debug.Log("OKEJMAG");
            //AudioManager.Instance.SwampPitch("GameMusic");
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Bamboo"){
            //Debug.Log("You can't climb anymore");
            GameManager.Instance.isClimbable = false;
            GameManager.Instance.setClimbObject(null);
        }else if(other.tag == "SwampTile"){
            GameManager.Instance.isSwamp = false;
        }else if(other.tag == "ArcticTile"){
            GameManager.Instance.isArctic = false;
        }
    }
}
