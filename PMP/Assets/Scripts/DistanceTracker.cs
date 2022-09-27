using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{

    [HideInInspector]
    public float distanceTravelled;
    public Transform player;

    public static DistanceTracker Instance;

    private Transform startPosition;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        distanceTravelled = 0;
        startPosition = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled = Mathf.Ceil(player.position.x - startPosition.position.x);
    }
}
