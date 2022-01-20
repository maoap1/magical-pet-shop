using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RecipeInfo : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI animalName;
    public GameObject resourcesPanel;
    public GameObject resourcePrefab;
    public GameObject progressPanel;
    public GameObject progressPrefab;
    public TextMeshProUGUI animalsProduced;
    public Image categoryImage;
    public Image secondaryCategoryImage;
    public TextMeshProUGUI level;
    public ResourceCost moneyValue;
    public ResourceCost craftTime;
    [HideInInspector]
    public List<GameObject> tmpHidden;

    private RecipeProgress recipe;

    public void Open(RecipeProgress rp)
    {
        GameLogic.THIS.inRecipeInfo = true;
        recipe = rp;
        this.gameObject.SetActive(true);

        this.GetComponent<SlidingTween>().SlideY();
        this.GetComponent<SlidingTween>().SetY(this.GetComponent<SlidingTween>().startY);
        if (this.GetComponent<SlidingTween>().enable) gameObject.SetActive(true);
        GetComponent<RectTransform>().DOAnchorPosY(this.GetComponent<SlidingTween>().targetY, this.GetComponent<SlidingTween>().duration)
            .SetEase(this.GetComponent<SlidingTween>().tweenType)
            .OnComplete(() => {
                foreach (GameObject g in tmpHidden)
                {
                    g.SetActive(false);
                }
            });

        secondaryCategoryImage.gameObject.SetActive(false);

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


        foreach (Transform child in progressPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i=0; i<rp.recipe.recipeLevels.Count; i++)
        {
            GameObject recipeLevel = Instantiate(progressPrefab, progressPanel.transform);
            recipeLevel.GetComponent<RecipeProgressionPanel>().Display(rp, i);
        }
        animalsProduced.text = recipe.animalsProduced.ToString();
        categoryImage.sprite = recipe.animal.category.artwork;
        if (recipe.animal.secondaryCategories.Count>0)
        {
            secondaryCategoryImage.sprite = recipe.animal.secondaryCategories[0].artwork;
            secondaryCategoryImage.gameObject.SetActive(true);
        }
        level.text = "T" + recipe.animal.level;
        moneyValue.SetNoRed();
        moneyValue.SetCost((int)(recipe.animal.value * recipe.costMultiplier));
        craftTime.SetCostTime(recipe.duration);
    }

    public void Close()
    {
        GameLogic.THIS.inRecipeInfo = false;
        GameLogic.THIS.currentRecipeCategory = null;
        this.GetComponent<SlidingTween>().SlideYBackCurve();
        foreach (GameObject g in tmpHidden)
        {
            g.SetActive(true);
        }
        tmpHidden.Clear();
    }
}
