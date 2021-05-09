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
    public void SetCost(Sprite icon, int cost)
    {
        this.icon.sprite = icon;
        costText.text = cost.ToString();
    }

    public void SetCost(InventoryAnimal inventoryAnimal)
    {
        this.icon.sprite = inventoryAnimal.animal.artwork;
        costText.text = inventoryAnimal.count.ToString();
        animalCost = inventoryAnimal;
        resourceType = ResourceType.Animal;
    }

    public void SetCost(InventoryArtifact inventoryArtifact)
    {
        this.icon.sprite = inventoryArtifact.artifact.artwork;
        costText.text = inventoryArtifact.count.ToString();
        artifactCost = inventoryArtifact;
        resourceType = ResourceType.Artifact;
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
    }

    public void SetCost(int money)
    {
        this.icon.sprite = GameGraphics.THIS.money;
        costText.text = money.ToString();
        moneyCost = money;
        resourceType = ResourceType.Money;
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
                        costText.color = Color.black;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
                case ResourceType.Artifact:
                    if (Inventory.HasInInventory(artifactCost) || !red)
                    {
                        costText.color = Color.black;
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
                        costText.color = Color.black;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
                case ResourceType.Money:
                    if (Inventory.HasInInventory(moneyCost) || !red)
                    {
                        costText.color = Color.black;
                    }
                    else
                    {
                        costText.color = Color.red;
                    }
                    break;
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
