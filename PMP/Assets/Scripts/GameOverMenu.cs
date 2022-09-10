using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    public GameObject gameOverMenu;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver){
            gameOverMenu.SetActive(true);
        }else gameOverMenu.SetActive(false);
    }
}
