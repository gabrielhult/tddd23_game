using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerUI : MonoBehaviour
{
    public GameObject dangerUI;
    public HazardBehaviour hazardBehaviour;


    void Start(){
        hazardBehaviour = hazardBehaviour.GetComponent<HazardBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hazardBehaviour.isClose && !GameManager.Instance.isGameOver){
            dangerUI.SetActive(true);
        }else dangerUI.SetActive(false);
    }
}
