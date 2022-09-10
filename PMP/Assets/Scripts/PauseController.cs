using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{

    public bool isPaused;
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;

            if(isPaused){
                Time.timeScale = 0;
                GamePaused.Invoke();
            }else{
                Time.timeScale = 1;
                GameResumed.Invoke();
            }
        }
    }
}
