using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager Instance;

    public GameObject[] BongeStyles;
    private int randomModelIndex;
    private GameObject chosenBonge;
    public LevelLoader levelLoader;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        BongeMainMenuStyle();
        levelLoader = levelLoader.GetComponent<LevelLoader>();
    }

    void Update(){ //Kan göras om enligt https://www.youtube.com/watch?v=Hn804Wgr3KE
        if(Input.GetKeyDown(KeyCode.P)){
            LoadGameScene("GameScene");
        }else if(Input.GetKeyDown(KeyCode.Q)){
            Quit();
        }
    }

    public void BongeMainMenuStyle(){
        randomModelIndex = Random.Range(0, BongeStyles.Length);
        chosenBonge = BongeStyles[randomModelIndex];

        chosenBonge.SetActive(true);
 
    }

    public void LoadGameScene(string sceneName){
        levelLoader.LevelLoad(sceneName);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Quit!");
    }
}
