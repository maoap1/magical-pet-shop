using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;
    public Image animalImage;
    public ResourceCost cost;
    private Customer customer;
    public void Open(Customer customer)
    {
        this.customer = customer;
        animalImage.sprite = customer.desiredAnimal.animal.artwork;
        cost.costText.text = ((int)(customer.desiredAnimal.animal.value * GameLogic.THIS.getRarityMultiplier(customer.desiredAnimal.rarity))).ToString();
        cost.icon.sprite = GameGraphics.THIS.money;
        LayoutRebuilder.ForceRebuildLayoutImmediate(cost.GetComponent<RectTransform>());
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(false);
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(true);
        }
    }

    public void Refuse()
    {
        Shop.RemoveCustomer(customer);
        Close();
    }

    public void Sell()
    {
        if (Inventory.HasInInventoryPrecise(customer.desiredAnimal))
        {
            Shop.SellTo(customer);
            Close();
        }
    }
}
