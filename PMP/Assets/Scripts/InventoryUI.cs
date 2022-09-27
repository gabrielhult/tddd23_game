using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InventoryUI : MonoBehaviour
{

    private TextMeshProUGUI bananaText;
    private TextMeshProUGUI distanceText;
    // Start is called before the first frame update
    void Start()
    {
        bananaText = GetComponent<TextMeshProUGUI>();
        distanceText = GetComponent<TextMeshProUGUI>();
    }

    public void updateBananaText(PlayerInventory playerInventory){
        bananaText.text = playerInventory.ScoreCounter.ToString();
    }

    public void updateDistanceText(PlayerInventory playerInventory){
        distanceText.text = playerInventory.DistanceCounter.ToString();
    }
}
