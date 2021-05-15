using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavBar : MonoBehaviour {

    [SerializeField]
    private InventoryUI inventory;
    [SerializeField]
    private PackLeadersUI packLeaders;

    [SerializeField]
    private InvButton inventoryButton;
    [SerializeField]
    private LeadersButton leadersButton;
    [SerializeField]
    List<NavBarSceneButton> sceneButtons;

    int customersCount = 0;
    bool justLoaded = true;

    public void ShowNotification(string sceneName) {
        if (sceneName != SceneManager.GetActiveScene().name) { 
            foreach (var button in this.sceneButtons) {
                if (button.sceneName == sceneName)
                    button.ShowNotification();
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        if (inventory != null)
            inventoryButton.SetInventory(inventory);
        if (packLeaders != null)
            leadersButton.SetPackLeaders(packLeaders);

        if (PlayerState.THIS != null) {
            RefreshNotifications();
            this.justLoaded = false;
        }
    }

    // Update is called once per frame
    void Update() {
        RefreshNotifications();
    }

    private void RefreshNotifications() {
        // initialize notifications
        string currentScene = SceneManager.GetActiveScene().name;
        // if the current scene is Yard and there is some finished crafting --> notification for Lab
        if (currentScene == "Yard" && PlayerState.THIS.crafting != null && PlayerState.THIS.crafting.Find(c => c.fillRate >= 1) != null) {
            ShowNotification("Lab");
        }
        // if the current scene is not Yard and there is some finished expedition --> notification for Yard
        if (currentScene != "Yard" && PlayerState.THIS.expeditions != null && PlayerState.THIS.expeditions.Find(e => e.fillRate >= 1) != null) {
            ShowNotification("Yard");
        }
        // if the current scene is not Shop and there are more customers then before --> notification for Shop
        if (currentScene != "Shop" && Shop.customers != null) {
            int count = 0;
            foreach (var customer in Shop.customers) {
                if (customer.hasValue) ++count;
            }
            if (count > this.customersCount && !this.justLoaded)
                ShowNotification("Shop");
            this.customersCount = count;
        }
        // the current scene should drop notification
        foreach (var button in this.sceneButtons) {
            if (button.sceneName == currentScene) { // active scene
                button.HideNotification();
            }
        }
        this.justLoaded = false;
    }
}
