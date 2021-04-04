using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeInfo : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI animalName;
    public GameObject resourcesPanel;
    public GameObject resourcePrefab;
    public TextMeshProUGUI animalsProduced;
    public Image categoryImage;
    public TextMeshProUGUI level;
    public ResourceCost moneyValue;
    public ResourceCost craftTime;
    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;
    [HideInInspector]
    public List<GameObject> tmpHidden;

    private RecipeProgress recipe;

    public void Open(RecipeProgress rp)
    {
        recipe = rp;
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in tmpHidden)
        {
            g.SetActive(false);
        }

        image.sprite = rp.animal.artwork;
        animalName.text = rp.animal.name;
        foreach (Transform child in resourcesPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (EssenceAmount e in recipe.costEssences)
        {
            GameObject costPart = Instantiate(resourcePrefab, resourcesPanel.transform);
            costPart.GetComponent<ResourceCost>().SetCost(e);
        }
        foreach (InventoryArtifact a in recipe.costArtifacts)
        {
            GameObject costPart = Instantiate(resourcePrefab, resourcesPanel.transform);
            costPart.GetComponent<ResourceCost>().SetCost(a);
        }
        foreach (InventoryAnimal a in recipe.costAnimals)
        {
            GameObject costPart = Instantiate(resourcePrefab, resourcesPanel.transform);
            costPart.GetComponent<ResourceCost>().SetCost(a);
        }
        animalsProduced.text = recipe.animalsProduced.ToString();
        categoryImage.sprite = recipe.animal.category.artwork;
        level.text = "T" + recipe.animal.level;
        moneyValue.SetCost(recipe.animal.value);
        craftTime.SetCostTime(recipe.duration);
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
        foreach (GameObject g in tmpHidden)
        {
            g.SetActive(false);
        }
        tmpHidden.Clear();
    }
}
