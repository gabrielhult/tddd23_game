using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{

    public GameObject gameplayMenu;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver){
            gameplayMenu.SetActive(false);
        }else gameplayMenu.SetActive(true);
    }
}
