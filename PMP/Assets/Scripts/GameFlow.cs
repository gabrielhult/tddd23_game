using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public Transform[] nextTile;
    public Transform[] traversableObjects;
    public Transform[] smallObjects;
    public Transform[] largeObjects;
    public Transform[] movingObjects;
    public Transform bananaInstance;
    public PlayerInventory playerInventory;
    public int bananaSpawnRateIndex;
    public int obstacleSpawnRateIndex;
    public int largeObjChance;
    public int movingObjChance;
    public int secondObjChance;
    public int secondMovingObjChance;
    public int maxBananaHeight;

    private int randObstacleIndex;
    private Vector3 nextMainTileSpawn;
    private Vector3 nextObstacleSpawn;
    private float currentVelocityX;
    private int tempRandZPos;
    private int randZPos;
    private int randYPos;
    private int obstacleSpawnCounter;
    private int bananaSpawnCounter;
    private bool isLargeObjectSpawned;
    private bool isMovableObjectSpawned;


    private int tileRand;
    private int holeSpawnCounter;
    public int holeSpawnRateIndex;
  



    // Start is called before the first frame update
    void Start()
    {
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        nextMainTileSpawn.x = 44;
        StartCoroutine(spawnTile());
    }

    void tileSpawn(){
        holeSpawnCounter++;
        if(holeSpawnCounter % holeSpawnRateIndex == 0){
            tileRand = Random.Range(0, nextTile.Length);
        }else tileRand = 0;

        Instantiate(nextTile[tileRand], nextMainTileSpawn, nextTile[tileRand].rotation);
        
        nextObstacleSpawn = nextMainTileSpawn;
    }

    void obstacleSpawn(){
        //Spawns an obstacle every "obstacleSpawnRateIndex" tile
        obstacleSpawnCounter++;
        if(obstacleSpawnCounter % obstacleSpawnRateIndex == 0){
            
            //For movable obstacles (IDEA: scale movingObjChance so more and more movable spawns once it can)
            if(playerInventory.DistanceCounter > 300f){ //Start spawning when score is over 300
                if(Random.Range(0, movingObjChance) == 0){ //movingObjChance here is just to increase unlikelyhood
                    locationObstacle();
                    movingObjectSpawn();
                    if(Random.Range(0, secondMovingObjChance) == 0){ //secondMovingObjChance here is just to increase unlikelyhood
                        //Makes sure two obstacles don't spawn at the same location
                        do{ 
                            tempRandZPos = randZPos;
                            locationObstacle();
                        }while(randZPos == tempRandZPos);
                        movingObjectSpawn();
                    }
                    isMovableObjectSpawned = true;
                }else isMovableObjectSpawned = false;
            }
            

            //For large obstacles
            if(Random.Range(0, largeObjChance) == 0 && !isMovableObjectSpawned){ //largeObjChance here is just to increase unlikelyhood
                largeObjectSpawn();
                isLargeObjectSpawned = true;
            }else isLargeObjectSpawned = false;
            
            //For small objects
            if(!isLargeObjectSpawned && !isMovableObjectSpawned){
                locationObstacle();
                traverseableObjectSpawn();
                if(Random.Range(0, secondObjChance) == 0){ //secondObjChance here is just to increase unlikelyhood
                    //Makes sure two obstacles don't spawn at the same location
                    do{ 
                        tempRandZPos = randZPos;
                        locationObstacle();
                    }while(randZPos == tempRandZPos);

                        smallObjectSpawn();
                }
            }
            obstacleSpawnCounter = 0;
        }
    }

    void locationObstacle(){
        //Randomly chooses where to place the next obstacle; left, center or right. -6 = left, 0 = center, 6 = right
        randZPos = Random.Range(-1, 2) * 6;
        nextObstacleSpawn.z = randZPos;
    }

    void traverseableObjectSpawn(){
        //Randomly chooses an obstacle type from traversableObjects array
        randObstacleIndex = Random.Range(0, traversableObjects.Length);
        //Spawns an object from the "Traversable Objects"-array.
        Instantiate(traversableObjects[randObstacleIndex], nextObstacleSpawn, traversableObjects[randObstacleIndex].rotation);
    }

    void smallObjectSpawn(){
        //Randomly chooses an obstacle type from smallObjects array
        randObstacleIndex = Random.Range(0, smallObjects.Length);
        //Spawns an object from the "Small Objects"-array.
        Instantiate(smallObjects[randObstacleIndex], nextObstacleSpawn, smallObjects[randObstacleIndex].rotation);
    }

    /* Large Objects should ALWAYS spawned in the middle */
    void largeObjectSpawn(){
        nextObstacleSpawn.z = 0;
        //Randomly chooses an obstacle type from largeObjects array
        randObstacleIndex = Random.Range(0, largeObjects.Length);
        //Spawns an object from the "Large Objects"-array.
        Instantiate(largeObjects[randObstacleIndex], nextObstacleSpawn, largeObjects[randObstacleIndex].rotation);
    }

    void movingObjectSpawn(){
        //nextObstacleSpawn.z = -4;
        //Randomly chooses an obstacle type from largeObjects array
        randObstacleIndex = Random.Range(0, movingObjects.Length);
        //Spawns an object from the "Large Objects"-array.
        Instantiate(movingObjects[randObstacleIndex], nextObstacleSpawn, movingObjects[randObstacleIndex].rotation);
    } 

    /* TODO: Fix so banana don't spawn inside a wall */
    void bananaSpawn(){ 
        bananaSpawnCounter++;
        if(bananaSpawnCounter % bananaSpawnRateIndex == 0){
            randYPos = Random.Range(2, maxBananaHeight);
            nextObstacleSpawn.y = randYPos;
            //Improve where these can spawn, right now only at empty, not taken lanes
            do{ 
                tempRandZPos = randZPos;
                locationObstacle();
            }while(randZPos == tempRandZPos);
            //Spawns a banana
            Instantiate(bananaInstance, nextObstacleSpawn, bananaInstance.rotation);
            //Resets y position so obstacles don't spawn weirdly
            //nextObstacleSpawn
        }
    }

    IEnumerator spawnTile(){
        //Logic for how often new tiles spawn
        yield return new WaitForSeconds(.33f / GameManager.Instance.gameplayScaleMultiplier); //Optimera detta
        if(!GameManager.Instance.isGameOver){
            tileSpawn();
            
            obstacleSpawn();

            bananaSpawn();

            nextMainTileSpawn.x += 4;
            StartCoroutine(spawnTile()); //Makes this IEnumerator loop.
        }
    }
}
