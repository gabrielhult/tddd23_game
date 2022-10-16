using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public GameObject thisObstacle;
    private Collider[] childColliders;
    private Renderer[] childRenderers;
    private Collider colliderElement;
    private Renderer rendererElement;
    private Material elementMaterial;
    private Color elementColor;
    private bool reset;


    void Awake(){
        reset = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        thisObstacle = this.gameObject;
        childColliders = thisObstacle.GetComponentsInChildren<Collider>();
        childRenderers = thisObstacle.GetComponentsInChildren<Renderer>();
    }

    void Update(){
        if(GameManager.Instance.chosenPowerUp == "NoObstacles" && GameManager.Instance.isPowerUp){
            //BananaModel, for color check fix, Banana for collider
            NoObstacleMode();
        }else if(!GameManager.Instance.isPowerUp && reset){
            ResetMode();
            reset = false;
        }
    }

    private void OnTriggerEnter(Collider other){ //TODO: Fixa detta, funkar ej ännu, vet ej varför dock
        if(other.tag == "Banana"){
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Hazard"){
            if(!GameManager.Instance.isGameOver){
                thisObstacle.SetActive(false);
             }
        }
    }

    public void NoObstacleMode(){
        reset = true;
         for (int index = 0; index < childColliders.Length; index++)
         {
            colliderElement = childColliders[index];
            if(colliderElement.gameObject.name != "Banana" && colliderElement != null){
                colliderElement.enabled = false;
            }
         }

         for (int index = 0; index < childRenderers.Length; index++)
         {
            rendererElement = childRenderers[index];
            if(rendererElement != null){
                if(rendererElement.gameObject.name != "BananaModel"){
                    elementColor = rendererElement.material.color;
                    elementColor.a = 0.5f;
                    rendererElement.material.color = elementColor;
                    rendererElement.material.shader = Shader.Find("Transparent/Diffuse");
                }
            }
            
            
         }
    }

    public void ResetMode(){
        for (int index = 0; index < childColliders.Length; index++)
         {
            colliderElement = childColliders[index];
            if(colliderElement.gameObject.name != "Banana" && colliderElement !=null){
                colliderElement.enabled = true;
            }
         }

         for (int index = 0; index < childRenderers.Length; index++)
         {
            rendererElement = childRenderers[index];
            if(rendererElement != null){
                if(rendererElement.gameObject.name != "BananaModel"){
                    elementColor = rendererElement.material.color;
                    elementColor.a = 1;
                    rendererElement.material.shader = Shader.Find("Standard");
                }
            }
         }
    }
}
