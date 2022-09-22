using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transistion;

    public float transitionTime;

    public void LevelLoad(string sceneName){
        StartCoroutine(LoadLevel(sceneName));
    }


    IEnumerator LoadLevel(string sceneName){
        transistion.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
