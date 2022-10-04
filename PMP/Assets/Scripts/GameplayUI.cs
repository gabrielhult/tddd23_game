using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{

    public GameObject gameplayMenu;
    public GameObject doubleBananaUI;
    public GameObject increaseDistanceUI;
    public GameObject noObstaclesUI;
    public GameObject slowHazardUI;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver){
            gameplayMenu.SetActive(false);
        }else gameplayMenu.SetActive(true);

        if(GameManager.Instance.chosenPowerUp == "IncreaseDistanceAward"){ //IEnumerator behövs för att inte logik och UI krockar
            Debug.Log("AWARD");      
            increaseDistanceUI.SetActive(true);     
            StartCoroutine(showDistanceAward());
        }else increaseDistanceUI.SetActive(false);

        if(GameManager.Instance.chosenPowerUp == "DoubleBananaScoreCollect"){
            doubleBananaUI.SetActive(true);
        }else doubleBananaUI.SetActive(false);

        if(GameManager.Instance.chosenPowerUp == "NoObstacles"){
            noObstaclesUI.SetActive(true);
        }else noObstaclesUI.SetActive(false);

        if(GameManager.Instance.chosenPowerUp == "SlowDownHazard"){
            slowHazardUI.SetActive(true);
        }else slowHazardUI.SetActive(false);
    }

    IEnumerator showDistanceAward(){

        yield return new WaitForSeconds(1);
        GameManager.Instance.chosenPowerUp = "";
        GameManager.Instance.distanceBonusLimiter = true;
        

    }
}
