using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreLoader : MonoBehaviour
{
    public TextMeshProUGUI bananaHighScore; 
    public TextMeshProUGUI distanceHighScore; 

    // Start is called before the first frame update
    void Start()
    {
        bananaHighScore.text = PlayerPrefs.GetInt("BananaHighScore", 0).ToString();
        distanceHighScore.text = PlayerPrefs.GetFloat("DistanceHighScore", 0).ToString();
    }

    public void ResetHighScore(){
        Debug.Log("Highscores resetted!");
        PlayerPrefs.DeleteKey("BananaHighScore");
        PlayerPrefs.DeleteKey("DistanceHighScore");
        bananaHighScore.text = "0";
        distanceHighScore.text = "0";
    }
}
