using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScore : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bananaNewRecordText;
    public TextMeshProUGUI distanceNewRecordText;
    private bool bananaScoreCounted;
    private bool distanceScoreCounted;

    private int currentScore;

    public int scoreIncrementRate;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        bananaScoreCounted = false;
        distanceScoreCounted = false;
        bananaNewRecordText.gameObject.SetActive(false);
        distanceNewRecordText.gameObject.SetActive(false);
    }

    public void CountEndScore(){
        StartCoroutine(CountScore());
    }


    IEnumerator CountScore(){

        yield return new WaitForSeconds(.005f);


        //Banana
        if(currentScore != playerInventory.ScoreCounter && !bananaScoreCounted){
            currentScore += scoreIncrementRate;
            bananaText.text = currentScore.ToString();
            StartCoroutine(CountScore());
        }else{
            if(!bananaScoreCounted){ //currentScore matches ScoreCounter
                if(playerInventory.bananaCelebrate && !bananaScoreCounted){
                    CelebrateBanana();
                    bananaNewRecordText.gameObject.SetActive(true);
                }
                bananaScoreCounted = true;
                bananaText.text = playerInventory.ScoreCounter.ToString();
                currentScore = 0;
            }
        }


        //Distance
        if(currentScore != playerInventory.DistanceCounter && bananaScoreCounted && !distanceScoreCounted){
            currentScore += scoreIncrementRate;
            distanceText.text = currentScore.ToString();
            StartCoroutine(CountScore());
        }else{ 
            if(!distanceScoreCounted){ //currentScore matches DistanceCounter
                if(playerInventory.distanceCelebrate && !distanceScoreCounted){
                    CelebrateDistance();
                    distanceNewRecordText.gameObject.SetActive(true);
                }
                distanceScoreCounted = true;
                distanceText.text = playerInventory.DistanceCounter.ToString();
                currentScore = 0;
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
