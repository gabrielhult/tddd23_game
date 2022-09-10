using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public PauseController pauseController;
    public GameObject pauseMenuMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseController = pauseController.GetComponent<PauseController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseController.isPaused){
            pauseMenuMenu.SetActive(true);
        }else pauseMenuMenu.SetActive(false);
    }
}
