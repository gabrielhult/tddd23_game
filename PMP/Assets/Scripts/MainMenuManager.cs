using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject[] BongeStyles;
    private int randomModelIndex;
    private GameObject chosenBonge;

    // Start is called before the first frame update
    void Start()
    {
        BongeMainMenuStyle();
    }

    public void BongeMainMenuStyle(){
        randomModelIndex = Random.Range(0, BongeStyles.Length);
        Debug.Log(randomModelIndex);
        chosenBonge = BongeStyles[randomModelIndex];
        Debug.Log(chosenBonge.name);

        chosenBonge.SetActive(true);
 
    }

    public void LoadGameScene(string sceneName){
        SceneManager.LoadScene(sceneName);
        Debug.Log("Load " + sceneName + "!");
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("Quit!");
    }
}
