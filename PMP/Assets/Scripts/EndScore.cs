using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI distanceText;
    public GameObject bananaNewRecordText;
    public GameObject distanceNewRecordText;
    private bool bananaScoreCounted;
    private bool distanceScoreCounted;

    private int currentScoreBanana;
    private int currentScoreDistance;
    private float scoreCountTimeScale;
    private int scoreAddTen = 10;

    public int scoreIncrementRate;
    // Start is called before the first frame update
    void Start()
    {
        currentScoreBanana = 0;
        currentScoreDistance = 0;
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        bananaScoreCounted = false;
        distanceScoreCounted = false;
        bananaNewRecordText.gameObject.SetActive(false);
        distanceNewRecordText.gameObject.SetActive(false);
    }

    public void CountEndScore(){
        StartCoroutine(CountScore());
    }


    IEnumerator CountScore(){ //Kan ej ha couroutine för detta?

        //TODO: Tweak constant values
        if(bananaScoreCounted){
            if(playerInventory.DistanceCounter == 0){
                scoreCountTimeScale = 2f;
            }else if(playerInventory.DistanceCounter < 100){
                scoreCountTimeScale = 2f / playerInventory.DistanceCounter;
            }else if(playerInventory.DistanceCounter < 1500){
                scoreCountTimeScale = 1f / playerInventory.DistanceCounter;
            }else scoreCountTimeScale = 0.5f / playerInventory.DistanceCounter;
        }else {
            if(playerInventory.ScoreCounter == 0){
                scoreCountTimeScale = 0.5f;
            }
            else if(playerInventory.ScoreCounter < 10){
                scoreCountTimeScale = 2f / playerInventory.ScoreCounter;
            }else if(playerInventory.ScoreCounter < 50){
                scoreCountTimeScale = 1f / playerInventory.ScoreCounter;
            }else scoreCountTimeScale = 0.5f / playerInventory.ScoreCounter;
        }

        //Debug.Log("Scale: " + scoreCountTimeScale);

        //Debug.Log("Before: " + Time.time);

        yield return new WaitForSeconds(scoreCountTimeScale);

        //Debug.Log("After: " + Time.time);

        //Banana
        if(currentScoreBanana != playerInventory.ScoreCounter && !bananaScoreCounted){
            if(playerInventory.ScoreCounter - currentScoreBanana  < scoreAddTen){
                currentScoreBanana += scoreIncrementRate;
            }else currentScoreBanana += scoreAddTen;
            
            bananaText.text = currentScoreBanana.ToString();
            StartCoroutine(CountScore());
        }else{
            if(!bananaScoreCounted){ //currentScore matches ScoreCounter
                if(playerInventory.bananaCelebrate){
                    CelebrateBanana();
                    bananaNewRecordText.SetActive(true);
                }
                bananaScoreCounted = true;
                bananaText.text = playerInventory.ScoreCounter.ToString();
            }
        }

        //Distance
        if(bananaScoreCounted){ //Only count distance if banana count is complete
            if(currentScoreDistance != playerInventory.DistanceCounter && !distanceScoreCounted){
                if(playerInventory.DistanceCounter - currentScoreDistance < 30){
                    currentScoreDistance += scoreIncrementRate;
                }else currentScoreDistance += scoreAddTen*3;
                
                distanceText.text = currentScoreDistance.ToString();
                StartCoroutine(CountScore());
            }else{ 
                if(!distanceScoreCounted){ //currentScore matches DistanceCounter
                    if(playerInventory.distanceCelebrate){
                        CelebrateDistance();
                        distanceNewRecordText.SetActive(true);
                    }
                    distanceScoreCounted = true;
                    distanceText.text = playerInventory.DistanceCounter.ToString();
                }
            }
        }
        
    }

    public void CelebrateBanana(){
        AudioManager.Instance.PlaySound("MankeCelebrate");
    }

    public void CelebrateDistance(){
        AudioManager.Instance.PlaySound("MankeCelebrate");
    }
}
