using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    public Transform[] nextTileDefault;
    public Transform[] nextTileArctic;
    public Transform[] nextTileMagma;
    public Transform[] nextTileSwamp;
    public Transform[] traversableObjects;
    public Transform[] smallObjects;
    public Transform[] largeObjects;
    public Transform[] movingObjects;
    public Transform bananaInstance;
    public PlayerInventory playerInventory;
    public int bananaSpawnRateIndex;
    public int obstacleSpawnRateIndex;
    public int biomeChangeRateIndex;
    public int largeObjChance;
    public int movingObjChance;
    public int secondObjChance;
    public int secondMovingObjChance;
    public int maxBananaHeight;
    public float largeObstacleThreshold;
    public float movingObstacleThreshold;
    public float secondMovingObstacleThreshold;
    public float secondSmallObstacleThreshold;
    public string[] biomeArray;
    [HideInInspector]
    public string chosenBiome;
    public int biomeRepeatLimit;

    
    private Transform[] tileArray;
    private int randObstacleIndex;
    private Vector3 nextMainTileSpawn;
    private Vector3 nextObstacleSpawn;
    private int tempRandZPos;
    private int randZPos;
    private int randYPos;
    private int obstacleSpawnCounter;
    private int bananaSpawnCounter;
    private bool isLargeObjectSpawned;
    private bool isMovableObjectSpawned;
    private int biomeRepeatCounter;
    private string compareBiome;
    private float biomeMultiplier;
    private float biomeChangeRateArctic;
    private float biomeChangeRateSwamp;
    private float biomeChangeRate;



    private int tileRand;
    private int holeSpawnCounter;
    public int holeSpawnRateIndex;
  



    // Start is called before the first frame update
    void Start()
    {
        playerInventory = playerInventory.GetComponent<PlayerInventory>();
        biomeChangeRateArctic = biomeChangeRateIndex + 20;
        biomeChangeRateSwamp = biomeChangeRateIndex - 20;
        nextMainTileSpawn.x = 44;
        tileArray = nextTileDefault;
        StartCoroutine(spawnTile());
    }

    void tileSpawn(){
        if(GameManager.Instance.isArctic){
            biomeChangeRate = biomeChangeRateArctic;
        }else if(GameManager.Instance.isSwamp){
            biomeChangeRate = biomeChangeRateSwamp;
        }else biomeChangeRate = biomeChangeRateIndex;

        if(playerInventory.DistanceCounter % biomeChangeRate < 2 && playerInventory.DistanceCounter > 3f){ //Kan s??kert fortfarande bli b??ttre men ??r okej
            chosenBiome = biomeArray[Random.Range(0, biomeArray.Length)];
            //If the same biome has been randomized too many times, force change it!!
            if(biomeRepeatCounter >= biomeRepeatLimit || GameManager.Instance.isSwamp){
                do{ 
                    chosenBiome = biomeArray[Random.Range(0, biomeArray.Length)];
                }while(chosenBiome == compareBiome);
                //Debug.Log("Limit reached, force changing biome to: " + chosenBiome);
                biomeRepeatCounter = 0;
            }else if(chosenBiome == compareBiome){
                biomeRepeatCounter++;
            }else biomeRepeatCounter = 0;

            //Debug.Log("New biome: " + chosenBiome + ", current streak: " + biomeRepeatCounter);

            if(chosenBiome == "Default"){
                tileArray = nextTileDefault;
                holeSpawnRateIndex = 3; 
                obstacleSpawnRateIndex = 4;
                compareBiome = "Default";
            }else if(chosenBiome == "Arctic"){
                tileArray = nextTileArctic;
                holeSpawnRateIndex = 3; 
                obstacleSpawnRateIndex = 4;
                compareBiome = "Arctic";
            }else if(chosenBiome == "Magma"){
                tileArray = nextTileMagma;
                holeSpawnRateIndex = 2; 
                obstacleSpawnRateIndex = 4;
                compareBiome = "Magma";
            }else if(chosenBiome == "Swamp"){
                tileArray = nextTileSwamp;
                holeSpawnRateIndex = 4; 
                obstacleSpawnRateIndex = 3;
                compareBiome = "Swamp";
            }
        }
        
        holeSpawnCounter++;
        if(holeSpawnCounter % holeSpawnRateIndex == 0){
            tileRand = Random.Range(0, tileArray.Length);
        }else tileRand = 0;

        Instantiate(tileArray[tileRand], nextMainTileSpawn, tileArray[tileRand].rotation);   
        nextObstacleSpawn = nextMainTileSpawn;
    }

    void obstacleSpawn(){
        //Spawns an obstacle every "obstacleSpawnRateIndex" tile
        obstacleSpawnCounter++;
        if(obstacleSpawnCounter % obstacleSpawnRateIndex == 0){
            
            //For movable obstacles
            if(playerInventory.DistanceCounter > movingObstacleThreshold){ //Start spawning when score is over movingObstacleThreshold
                if(Random.Range(0, movingObjChance) == 0){ //movingObjChance here is just to increase unlikelyhood
                    locationObstacle();
                    movingObjectSpawn();
                    if(Random.Range(0, secondMovingObjChance) == 0 && playerInventory.DistanceCounter > secondMovingObstacleThreshold){ //secondMovingObjChance here is just to increase unlikelyhood
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
            if(Random.Range(0, largeObjChance) == 0 && playerInventory.DistanceCounter > largeObstacleThreshold && !isMovableObjectSpawned){ //largeObjChance here is just to increase unlikelyhood
                largeObjectSpawn();
                isLargeObjectSpawned = true;
            }else isLargeObjectSpawned = false;
            
            //For small objects
            if(!isLargeObjectSpawned){  //&& !isMovableObjectSpawned..?
                locationObstacle();
                traverseableObjectSpawn();
                if(Random.Range(0, secondObjChance) == 0 && playerInventory.DistanceCounter > secondSmallObstacleThreshold){ //secondObjChance here is just to increase unlikelyhood
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
        //Randomly chooses an obstacle type from movingObjects array
        randObstacleIndex = Random.Range(0, movingObjects.Length);
        //Spawns an object from the "Moving Objects"-array.
        Instantiate(movingObjects[randObstacleIndex], nextObstacleSpawn, movingObjects[randObstacleIndex].rotation);
    } 

    void bananaSpawn(){ 
        bananaSpawnCounter++;
        if(bananaSpawnCounter % bananaSpawnRateIndex == 0){
            randYPos = Random.Range(2, maxBananaHeight);
            nextObstacleSpawn.y = randYPos;
            do{ 
                tempRandZPos = randZPos;
                locationObstacle();
            }while(randZPos == tempRandZPos);
            //Spawns a banana
            Instantiate(bananaInstance, nextObstacleSpawn, bananaInstance.rotation);
        }
    }

    IEnumerator spawnTile(){
        //Logic for how often new tiles spawn depending on biome
        if(GameManager.Instance.isArctic){
            biomeMultiplier = 1.2f;
        }else if(GameManager.Instance.isSwamp){
            biomeMultiplier = 0.8f;
        }else biomeMultiplier = 1;

        if(!GameManager.Instance.isGameOver){ //Split this up so banana can detect if it spawns inside obstacle collider
            tileSpawn();
            
            obstacleSpawn();
        }

        yield return new WaitForSeconds(.33f / (GameManager.Instance.gameplayScaleMultiplier * biomeMultiplier)); 

        if(!GameManager.Instance.isGameOver){ 

            bananaSpawn();

            nextMainTileSpawn.x += 4;
            StartCoroutine(spawnTile()); //Makes this IEnumerator loop.
        }
    }
}
