using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvButton : MonoBehaviour
{
    private InventoryUI _inventoryUI;

    public void SetInventory(InventoryUI inventory) {
        this._inventoryUI = inventory;
    }

    public void ShowInventory() {
        this._inventoryUI.Open();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
