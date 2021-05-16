using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerDisplayTutorial : MonoBehaviour
{
    public GameObject character;
    public Image order;
    public OrderPanel orderPanel;
    public Animal animal;

    private bool hidden;
    private bool justLoaded;

    private Customer customer;


    public void CustomerClicked()
    {
        if (customer.hasValue)
        {
            orderPanel.Open(customer);
        }
    }

    private void Start()
    {
        customer = new Customer();
        customer.desiredAnimal = new InventoryAnimal();
        customer.desiredAnimal.animal = animal;
        customer.desiredAnimal.count = 1;
        customer.desiredAnimal.rarity = Rarity.Common;
        customer.hasValue = true;

        this.hidden = true;
        this.justLoaded = true;
    }

    private void Update()
    {
        if (customer.hasValue && this.hidden)
        {
            InventoryAnimal desiredAnimal = customer.desiredAnimal;
            order.sprite = desiredAnimal.animal.artwork;
            order.material = new Material(order.material);
            order.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(desiredAnimal.rarity));
            order.material.SetTexture("_BloomTex", desiredAnimal.animal.bloomSprite.texture);
            character.SetActive(true);
            if (!this.justLoaded)
            {
                FindObjectOfType<AudioManager>().Play(SoundType.CustomerAppear);
            }
            this.hidden = false;
            this.justLoaded = false;
        }
    }
}
