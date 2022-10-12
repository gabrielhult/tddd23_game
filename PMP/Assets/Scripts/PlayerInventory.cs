using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int ScoreCounter {get; private set; } //Any script can get the value but only this script can set the value
    public float DistanceCounter {get; private set;}
    public UnityEvent<PlayerInventory> onBananaCollected;
    public UnityEvent<PlayerInventory> onDistanceTravelled;
    [HideInInspector]
    public bool bananaCelebrate;
    [HideInInspector]
    public bool distanceCelebrate;

    public void ScoreCollected(){
        //IDEA: x amount of score gives an extra life?
        if(GameManager.Instance.chosenPowerUp == "DoubleBananaScoreCollect" && GameManager.Instance.isPowerUp){
            ScoreCounter = ScoreCounter + 2;
        }else ScoreCounter++;
        onBananaCollected.Invoke(this);
    }

    public void AwardDistanceScore(){
        DistanceCounter = DistanceTracker.Instance.distanceTravelled;
        onDistanceTravelled.Invoke(this);
    }

    public void BananaHighScoreCheck(){
        //TODO: Counter for end score at GameOverUI: https://www.youtube.com/watch?v=i1KmYHoXx80
        if(ScoreCounter > PlayerPrefs.GetInt("BananaHighScore", 0)){
            PlayerPrefs.SetInt("BananaHighScore", ScoreCounter);
            bananaCelebrate = true; //Effects in EndScore
        }
    }

    public void DistanceHighScoreCheck(){
        //TODO: Counter for end score at GameOverUI: https://www.youtube.com/watch?v=i1KmYHoXx80
        if(DistanceCounter > PlayerPrefs.GetFloat("DistanceHighScore", 0)){
            PlayerPrefs.SetFloat("DistanceHighScore", DistanceCounter);
            distanceCelebrate = true; //Effects in EndScore
        }
    }
}
