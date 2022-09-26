using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunUI : MonoBehaviour
{
    public GameObject autorunUI;
    public PlayerBehaviour playerBehaviour;


    void Start(){
        playerBehaviour = playerBehaviour.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerBehaviour.autorun && !GameManager.Instance.isGameOver){
            autorunUI.SetActive(true);
        }else autorunUI.SetActive(false);
    }
}
