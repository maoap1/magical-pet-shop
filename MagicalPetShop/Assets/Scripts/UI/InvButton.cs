using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvButton : MonoBehaviour
{
    private GameObject _inventory;
    private InventoryUI _inventoryUI;

    public void SetInventory(GameObject inventory) {
        this._inventory = inventory;
        this._inventoryUI = inventory.GetComponent<InventoryUI>();
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
