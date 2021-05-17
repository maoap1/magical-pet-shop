using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCost : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI costText;

    private InventoryAnimal animalCost;
    private InventoryArtifact artifactCost;
    private EssenceAmount essenceCost;
    private int moneyCost;
    private ResourceType resourceType = ResourceType.Money;
    private float updateTime = 0;
    private bool red = true;

    private Color defaultTextColor;

    public void SetCost(Sprite icon, int cost)
    {
        this.icon.sprite = icon;
        costText.text = cost.ToString();
        InitializeColor();
    }

    public void SetCost(InventoryAnimal inventoryAnimal)
    {
        this.icon.sprite = inventoryAnimal.animal.artwork;
        costText.text = inventoryAnimal.count.ToString();
        animalCost = inventoryAnimal;
        resourceType = ResourceType.Animal;
        InitializeColor();
    }

    public void SetCost(InventoryArtifact inventoryArtifact)
    {
        this.icon.sprite = inventoryArtifact.artifact.artwork;
        costText.text = inventoryArtifact.count.ToString();
        artifactCost = inventoryArtifact;
        resourceType = ResourceType.Artifact;
        InitializeColor();
    }

    public void SetCost(EssenceAmount essenceAmount)
    {
        //if (PlayerState.THIS.resources.Find(e => e.essence == essenceAmount.essence && e.unlocked) != null)
        //{
        this.icon.sprite = essenceAmount.essence.icon;
        //}
        //else
        //{
        //    this.icon.sprite = GameGraphics.THIS.unknown;
        //}
        costText.text = essenceAmount.amount.ToString();
        essenceCost = essenceAmount; 
        resourceType = ResourceType.Essence;
        InitializeColor();
    }

    public void SetCost(int money)
    {
        this.icon.sprite = GameGraphics.THIS.money;
        costText.text = money.ToString();
        moneyCost = money;
        resourceType = ResourceType.Money;
        InitializeColor();
    }

    public void SetNoRed()
    {
        red = false;
    }

    public void SetCostTime(int value)
    {
        this.icon.sprite = GameGraphics.THIS.time;
        string text = (value % 60).ToString() + "s";
        value /= 60;
        if (value != 0)
        {
            text = (value % 60).ToString() + "min " + text;
        }
        value /= 60;
        if (value != 0)
        {
            text = (value).ToString() + "h " + text;
        }
        costText.text = text;
        resourceType = ResourceType.Other;
        InitializeColor();
    }

    public void Update()
    {
        if (Time.time - updateTime > 1)
        {
            updateTime = Time.time;
            switch (resourceType)
            {
                case ResourceType.Animal:
                    if (Inventory.HasInInventory(animalCost) || !red)
                    {
                        costText.color = this.defaultTextColor;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
                case ResourceType.Artifact:
                    if (Inventory.HasInInventory(artifactCost) || !red)
                    {
                        costText.color = this.defaultTextColor;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
                case ResourceType.Essence:
                    if (PlayerState.THIS.resources.Find(e => e.essence == essenceCost.essence && e.unlocked) != null)
                    {
                        this.icon.sprite = essenceCost.essence.icon;
                    }
                    else
                    {
                        this.icon.sprite = GameGraphics.THIS.unknown;
                    }
                    if (Inventory.HasInInventory(essenceCost) || !red)
                    {
                        costText.color = this.defaultTextColor;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
                case ResourceType.Money:
                    if (Inventory.HasInInventory(moneyCost) || !red)
                    {
                        costText.color = this.defaultTextColor;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
            }
        }
    }

    private void InitializeColor() {
        TMPColor colorComponent = costText.gameObject.GetComponent<TMPColor>();
        if (colorComponent != null)
            this.defaultTextColor = UIPalette.THIS.GetColor(colorComponent.color);
        else { 
            TextColor colorComponent2 = costText.gameObject.GetComponent<TextColor>();
            if (colorComponent2 != null) {
                this.defaultTextColor = UIPalette.THIS.GetColor(colorComponent2.color);
            } else {
                this.defaultTextColor = Color.black;
            }
        }
    }
}

public enum ResourceType
{
    Animal,
    Artifact,
    Essence,
    Money,
    Other
}
