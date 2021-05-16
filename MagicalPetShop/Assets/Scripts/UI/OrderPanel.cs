using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AppearHideComponent))]
public class OrderPanel : MonoBehaviour
{
    public Button sellButton;
    public TextMeshProUGUI sellButtonText;
    public Image animalImage;
    public TextMeshProUGUI animalText;
    public ResourceCost cost;
    private Customer customer;
    public void Open(Customer customer)
    {
        this.customer = customer;
        animalImage.sprite = customer.desiredAnimal.animal.artwork;
        animalImage.material = new Material(animalImage.material);
        animalImage.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(customer.desiredAnimal.rarity));
        animalImage.material.SetTexture("_BloomTex", customer.desiredAnimal.animal.bloomSprite.texture);
        animalText.text = customer.desiredAnimal.animal.name;
        cost.costText.text = ((int)(customer.desiredAnimal.animal.value * GameLogic.THIS.getRarityMultiplier(customer.desiredAnimal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == customer.desiredAnimal.animal).costMultiplier)).ToString();
        cost.icon.sprite = GameGraphics.THIS.money;
        cost.SetNoRed();
        if (!Inventory.HasInInventoryPrecise(customer.desiredAnimal)) {
            sellButton.interactable = false;
            sellButton.gameObject.GetComponent<Image>().color = UIPalette.THIS.GetColor(PaletteColor.Inactive);
            sellButtonText.color = Color.red;
        } else {
            sellButton.interactable = true;
            sellButton.gameObject.GetComponent<Image>().color = UIPalette.THIS.GetColor(sellButton.gameObject.GetComponent<ImageColor>().color);
            sellButtonText.color = UIPalette.THIS.GetColor(sellButtonText.gameObject.GetComponent<TMPColor>().color);
        }
        this.gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(cost.GetComponent<RectTransform>());
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide)
        {
            g.SetActive(false);
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide)
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
