using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Borde göra att jag kan ta den varifrån somhelst
    
    public GameObject goalLine;
    public CameraBehaviour cameraBehaviour;
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
    }

    

    public void NextLevel(){
        changePlayerAngleAndDir = true;
        levelCount++;
        cameraBehaviour.rotateCamera = true;
    }

    public void CollectBanana(){
        Debug.Log("Banana");
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
