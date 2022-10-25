using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    public GameObject gameOverMenu;
    public GameObject muteUI;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver){
            gameOverMenu.SetActive(true);
        }else gameOverMenu.SetActive(false);

        if(MuteManager.Instance.isMuted){
            muteUI.SetActive(true);
        }else muteUI.SetActive(false);
    }
}
