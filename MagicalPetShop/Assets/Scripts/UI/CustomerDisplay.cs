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

    private bool hidden;
    private bool justLoaded;


    private void OnMouseDown()
    {
        if (!Utils.IsPointerOverGameObject() && Shop.customers[CustomerID].hasValue)
        {
            orderPanel.Open(Shop.customers[CustomerID]);
        }
    }

    private void Start() {
        this.hidden = true;
        this.justLoaded = true;
    }

    private void Update()
    {
        if (Shop.customers != null && (Shop.customers[CustomerID]==null || !Shop.customers[CustomerID].hasValue))
        {
            character.SetActive(false);
            this.hidden = true;
            if (Shop.customers != null && Shop.customers[CustomerID] != null && !Shop.customers[CustomerID].hasValue)
                this.justLoaded = false;
        }
        else if (Shop.customers != null && Shop.customers[CustomerID].hasValue && this.hidden)
        {
            InventoryAnimal desiredAnimal = Shop.customers[CustomerID].desiredAnimal;
            order.sprite = desiredAnimal.animal.artwork;
            order.material = new Material(order.material);
            order.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(desiredAnimal.rarity));
            order.material.SetTexture("_BloomTex", desiredAnimal.animal.bloomSprite.texture);
            character.SetActive(true);
            if (!this.justLoaded) {
                FindObjectOfType<AudioManager>().Play(SoundType.CustomerAppear);
            }
            this.hidden = false;
            this.justLoaded = false;
        }
    }
}
