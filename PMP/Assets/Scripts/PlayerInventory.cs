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

    public void ScoreCollected(){
        //IDEA: x amount of score gives an extra life?
        ScoreCounter++;
        onBananaCollected.Invoke(this);
    }

    public void AwardDistanceScore(){
        DistanceCounter = DistanceTracker.Instance.distanceTravelled;
        onDistanceTravelled.Invoke(this);
    }

    public void BananaHighScoreCheck(){
        //Debug.Log("Pre Score: " + PlayerPrefs.GetInt("ScoreHighScore", 0));
        if(ScoreCounter > PlayerPrefs.GetInt("BananaHighScore", 0)){
            PlayerPrefs.SetInt("BananaHighScore", ScoreCounter);
            //Celebration call?
        }
        //Debug.Log("Score: " + PlayerPrefs.GetInt("BananaHighScore", 0));
    }

    public void DistanceHighScoreCheck(){
        //Debug.Log("Pre Distance: " + PlayerPrefs.GetFloat("DistanceHighScore", 0));
        if(DistanceCounter > PlayerPrefs.GetFloat("DistanceHighScore", 0)){
            PlayerPrefs.SetFloat("DistanceHighScore", DistanceCounter);
            //Celebration call?
        }
        //Debug.Log("Distance: " + PlayerPrefs.GetFloat("DistanceHighScore", 0));
    }
}
