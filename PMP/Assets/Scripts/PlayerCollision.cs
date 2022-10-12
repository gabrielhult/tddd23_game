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
        }
        else if(other.tag == "DefaultTile"){ //TODO: Kolla om pitchen kan justeras, annars ha helt olika instanser av samma ljud men olika pitch
            /* if(!GameManager.Instance.isDefault){
                AudioManager.Instance.PlaySound("GameMusicDefault");
                AudioManager.Instance.StopSound("GameMusicArctic");
                AudioManager.Instance.StopSound("GameMusicMagma");
                AudioManager.Instance.StopSound("GameMusicSwamp");
            } */
            GameManager.Instance.isDefault = true;
            GameManager.Instance.isArctic = false;
            GameManager.Instance.isSwamp = false;
            GameManager.Instance.isMagma = false;
        }
        else if(other.tag == "ArcticTile"){
            /* if(!GameManager.Instance.isArctic){
                AudioManager.Instance.PlaySound("GameMusicArctic");
                AudioManager.Instance.StopSound("GameMusicDefault");
                AudioManager.Instance.StopSound("GameMusicMagma");
                AudioManager.Instance.StopSound("GameMusicSwamp");
            } */
            GameManager.Instance.isArctic = true;
            GameManager.Instance.isSwamp = false;
            GameManager.Instance.isMagma = false;
            GameManager.Instance.isDefault = false;
        }
        else if(other.tag == "MagmaTile"){
            /* if(!GameManager.Instance.isMagma){
                AudioManager.Instance.PlaySound("GameMusicMagma");
                AudioManager.Instance.StopSound("GameMusicArctic");
                AudioManager.Instance.StopSound("GameMusicDefault");
                AudioManager.Instance.StopSound("GameMusicSwamp");
            } */
            GameManager.Instance.isMagma = true;
            GameManager.Instance.isArctic = false;
            GameManager.Instance.isSwamp = false;
            GameManager.Instance.isDefault = false;
        }
        else if(other.tag == "SwampTile"){
            /* if(!GameManager.Instance.isSwamp){
                AudioManager.Instance.PlaySound("GameMusicSwamp");
                AudioManager.Instance.StopSound("GameMusicArctic");
                AudioManager.Instance.StopSound("GameMusicMagma");
                AudioManager.Instance.StopSound("GameMusicDefault");
            } */
            GameManager.Instance.isSwamp = true;
            GameManager.Instance.isArctic = false;
            GameManager.Instance.isMagma = false;
            GameManager.Instance.isDefault = false;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Bamboo"){
            GameManager.Instance.isClimbable = false;
            GameManager.Instance.setClimbObject(null);
        }
    }
}
