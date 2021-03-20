using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour {

    [SerializeField]
    private string location1;
    [SerializeField]
    private string scene1;
    [SerializeField]
    private Button button1;

    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private Button inventoryButton;

    [SerializeField]
    private string location2;
    [SerializeField]
    private string scene2;
    [SerializeField]
    private Button button2;

    // Start is called before the first frame update
    void Start() {
        button1.GetComponent<NavButton>().SetScene(scene1);
        button1.GetComponentInChildren<Text>().text = location1;

        inventoryButton.GetComponent<InvButton>().SetInventory(inventory);

        button2.GetComponent<NavButton>().SetScene(scene2);
        button2.GetComponentInChildren<Text>().text = location2;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
