using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;

    public GameObject[] MankeStyles;
    public GameObject muteUI;
    private int randomModelIndex;
    private GameObject chosenManke;
    public LevelLoader levelLoader;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        AudioManager.Instance.PlaySound("MenuMusic");
        if(MankeStyles.Length != 0){
            MankeMainMenuStyle();
        }else Debug.Log("No Manke Styles Available! Range is 0!");
        levelLoader = levelLoader.GetComponent<LevelLoader>();
    }

    void Update(){ //Kan göras om enligt https://www.youtube.com/watch?v=Hn804Wgr3KE

        if(MuteManager.Instance.isMuted){
            muteUI.SetActive(true);
        }else muteUI.SetActive(false);

        if(Input.GetKeyDown(KeyCode.P)){
            AudioManager.Instance.PlaySound("ButtonClick");
            LoadGameScene("GameScene");
        }else if(Input.GetKeyDown(KeyCode.H)){
            if(SceneManager.GetActiveScene().name != "HighScoreScene"){
                AudioManager.Instance.PlaySound("ButtonClick");
                LoadGameScene("HighScoreScene");
            }
        }else if(Input.GetKeyDown(KeyCode.Q)){
            Quit();
        }
    }

    public void playButtonClick(){
        AudioManager.Instance.PlaySound("ButtonClick");
    }


    public void MankeMainMenuStyle(){
        randomModelIndex = Random.Range(0, MankeStyles.Length);
        chosenManke = MankeStyles[randomModelIndex];

        chosenManke.SetActive(true);
 
    }

    public void LoadGameScene(string sceneName){
        levelLoader.LevelLoad(sceneName);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Quit!");
    }
}
