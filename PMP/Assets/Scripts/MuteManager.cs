using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteManager : MonoBehaviour
{
    public static MuteManager Instance;
    public bool isMuted;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        isMuted = PlayerPrefs.GetInt("Muted") == 1;
        if(isMuted){
            AudioListener.volume = 0f;
        }else AudioListener.volume = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            isMuted = !isMuted;
            if(isMuted){
                AudioListener.volume = 0f;
            }else AudioListener.volume = 1f;
            PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        }
    }
}
