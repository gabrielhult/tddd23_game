using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isPaused){
            pauseMenu.SetActive(true);
        }else {
            pauseMenu.SetActive(false);
        }
    }
}
