using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int ScoreCounter {get; private set; } //Any script can get the value but only this script can set the value
    public UnityEvent<PlayerInventory> onBananaCollected;

    public void ScoreCollected(){
        //IDEA: x amount of score gives an extra life?
        ScoreCounter++;
        onBananaCollected.Invoke(this);
    }
}
