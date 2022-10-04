using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBehaviour : MonoBehaviour
{
    public GameObject thisBanana;

    // Start is called before the first frame update
    void Start()
    {
        thisBanana = this.gameObject;
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Hazard"){
            if(!GameManager.Instance.isGameOver){
                Destroy(thisBanana);
            }
        }
    }
}
