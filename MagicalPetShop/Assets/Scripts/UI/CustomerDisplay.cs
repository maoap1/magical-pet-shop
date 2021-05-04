using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerDisplay : MonoBehaviour
{
    public int CustomerID;
    public GameObject character;
    public SpriteRenderer order;
    public OrderPanel orderPanel;

    private bool justLoaded = false;

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Shop.customers[CustomerID].hasValue)
        {
            orderPanel.Open(Shop.customers[CustomerID]);
        }
    }

    private void Start() {
        this.justLoaded = true;
    }

    private void Update()
    {
        if (Shop.customers[CustomerID]==null || !Shop.customers[CustomerID].hasValue)
        {
            character.SetActive(false);
        }
        else if (Shop.customers[CustomerID].hasValue)
        {
            order.sprite = Shop.customers[CustomerID].desiredAnimal.animal.artwork;
            if (!character.activeInHierarchy & !this.justLoaded) FindObjectOfType<AudioManager>().Play(AudioType.CustomerAppear);
            character.SetActive(true);
        }
        this.justLoaded = false;
    }
}
