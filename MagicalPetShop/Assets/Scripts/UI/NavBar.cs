using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour {

    [SerializeField]
    private InventoryUI inventory;
    [SerializeField]
    private PackLeadersUI packLeaders;

    [SerializeField]
    private InvButton inventoryButton;
    [SerializeField]
    private LeadersButton leadersButton;

    // Start is called before the first frame update
    void Start() {
        if (inventory != null)
            inventoryButton.SetInventory(inventory);
        if (packLeaders != null)
            leadersButton.SetPackLeaders(packLeaders);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
