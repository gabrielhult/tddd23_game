using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InventoryUI : MonoBehaviour
{

    private TextMeshProUGUI bananaText;
    // Start is called before the first frame update
    void Start()
    {
        bananaText = GetComponent<TextMeshProUGUI>();
    }

    public void updateBananaText(PlayerInventory playerInventory){
        bananaText.text = playerInventory.ScoreCounter.ToString();
    }
}
