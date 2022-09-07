using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Borde göra att jag kan ta den varifrån somhelst
    
    public GameObject goalLine;
    public int levelCount;
    public bool changePlayerAngleAndDir;
    public CameraBehaviour cameraBehaviour;
    public bool isGameOver;
    

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
}
