using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int ScoreCounter {get; private set; } //Any script can get the value but only this script can set the value
    public float DistanceCounter {get; private set;}
    public int distanceAdder;
    public UnityEvent<PlayerInventory> onBananaCollected;
    public UnityEvent<PlayerInventory> onDistanceTravelled;

    public void ScoreCollected(){
        //IDEA: x amount of score gives an extra life?
        ScoreCounter++;
        onBananaCollected.Invoke(this);
    }

    public void AwardDistanceScore(){
        DistanceCounter = DistanceTracker.Instance.distanceTravelled;
        onDistanceTravelled.Invoke(this);
    }
}
