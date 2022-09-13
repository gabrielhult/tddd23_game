using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public Transform firstMainTile;
    public Transform[] obstacleObjects;
    private int randObstacleIndex;
    public PlayerBehaviour playerBehaviour;
    private Vector3 nextMainTileSpawn;
    private Vector3 nextObstacleSpawn;
    private float currentVelocityX;
    private int randZObstacleIndex;
    private int stepValue = 6;
    private int roundValue;
    private int adjustedValue;



    // Start is called before the first frame update
    void Start()
    {
        nextMainTileSpawn.x = 44;
        StartCoroutine(spawnMainTile());
        playerBehaviour = playerBehaviour.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnMainTile(){
        //Logic for how often new tiles spawn.
        if(playerBehaviour.playerVelocity.x < 0.5){
            currentVelocityX = 1;
        }else currentVelocityX = playerBehaviour.playerVelocity.x;
        yield return new WaitForSeconds(1 / (currentVelocityX / 1.4f)); //Optimera detta

        //Randomly places an obstacle
        randZObstacleIndex = Random.Range(-6, 7);
        
        //Rounds value so that obstacle spawns at the different roads
        roundValue = ((int)Mathf.Floor(randZObstacleIndex / stepValue));
        adjustedValue = stepValue * roundValue;
        nextObstacleSpawn = nextMainTileSpawn;
        nextObstacleSpawn.z = adjustedValue;

        //Randomly chooses an obstacle type from GameManager array
        randObstacleIndex = Random.Range(0, obstacleObjects.Length);

        Instantiate(obstacleObjects[randObstacleIndex], nextObstacleSpawn, obstacleObjects[randObstacleIndex].rotation);
        Instantiate(firstMainTile, nextMainTileSpawn, firstMainTile.rotation);
        nextMainTileSpawn.x += 4;
        StartCoroutine(spawnMainTile());
    }
}
