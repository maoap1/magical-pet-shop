using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderPanel : MonoBehaviour
{
    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;
    public Button sellButton;
    public TextMeshProUGUI sellButtonText;
    public Image animalImage;
    public ResourceCost cost;
    private Customer customer;
    public void Open(Customer customer)
    {
        this.customer = customer;
        animalImage.sprite = customer.desiredAnimal.animal.artwork;
        animalImage.material = new Material(animalImage.material);
        animalImage.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(customer.desiredAnimal.rarity));
        animalImage.material.SetTexture("_BloomTex", customer.desiredAnimal.animal.bloomSprite.texture);
        cost.costText.text = ((int)(customer.desiredAnimal.animal.value * GameLogic.THIS.getRarityMultiplier(customer.desiredAnimal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == customer.desiredAnimal.animal).costMultiplier)).ToString();
        cost.icon.sprite = GameGraphics.THIS.money;
        if (!Inventory.HasInInventoryPrecise(customer.desiredAnimal)) {
            sellButton.interactable = false;
            sellButtonText.color = Color.red;
        } else {
            sellButton.interactable = true;
            sellButtonText.color = Color.black;
        }
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
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            Close();
        }
    }
}
