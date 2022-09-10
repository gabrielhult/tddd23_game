using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            //Pause the game if it isn't paused and vice versa
            GameManager.Instance.changePauseState();

            if(GameManager.Instance.isPaused){
                GameManager.Instance.Pause();
            }else{
                GameManager.Instance.Resume();
            }
        }
    }
}

