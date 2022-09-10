using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Borde göra att jag kan ta den varifrån somhelst
    
    //public GameObject goalLine;
    public CameraBehaviour cameraBehaviour;
    public PlayerInventory playerInventory;
    public int levelCount;
    public bool changePlayerAngleAndDir;
    public bool isGameOver;
    public bool isClimbable;
    public GameObject climbObject;
    

    private void Awake() {
        Instance = this;
        isGameOver = false;
    }

    void Start(){
        cameraBehaviour = cameraBehaviour.GetComponent<CameraBehaviour>();
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
    }

    
    public void NextLevel(GameObject gameObject){
        changePlayerAngleAndDir = true;
        levelCount++;
        cameraBehaviour.rotateCamera = true;
        gameObject.SetActive(false);
    }

    public void CollectScore(GameObject gameObject){
        if(playerInventory != null){
            playerInventory.ScoreCollected();
            gameObject.SetActive(false);
        }
        //Debug.Log(playerInventory.ScoreCounter);
    }

    public void GameOver(){
        isGameOver = true;
    }

    public GameObject getClimbObject(){
        return climbObject;
    }

    public void setClimbObject(GameObject gameObject){
        climbObject = gameObject;
    }

}
