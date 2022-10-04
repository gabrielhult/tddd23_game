using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceBehaviour : MonoBehaviour
{

    public GameObject thisTile;

    // Start is called before the first frame update
    void Start()
    {
        thisTile = this.gameObject;
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Hazard"){
            if(!GameManager.Instance.isGameOver){
                thisTile.SetActive(false);
             }
        }
    }
}
