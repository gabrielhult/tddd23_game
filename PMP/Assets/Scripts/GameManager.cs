using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Borde göra att jag kan ta den varifrån somhelst

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    

    public void NextLevel(){
        Debug.Log("Next Level!"); //funkar ej atm
    }
}
