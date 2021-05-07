using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergingImage : MonoBehaviour
{
    public MergingPanel mergingPanel;
    [HideInInspector]
    public InventoryAnimal animal;
    private float updateTime = 0;
    public void Clicked()
    {
        if (Crafting.CanStartMerging(animal))
        {
            int recipesBefore = PlayerState.THIS.recipes.Count;
            Crafting.StartMerging(animal);
            mergingPanel.mergingPanel.defaultMergingCategory.Display();
            mergingPanel.UpdateInfo();
        }
    }
    private void Update()
    {
        if (animal != null && Time.time - updateTime > 0.5)
        {
            updateGrayscale();
        }
    }

    private void Start()
    {
        gameObject.GetComponent<Image>().material = new Material(gameObject.GetComponent<Image>().material);
    }

    public void updateGrayscale()
    {
        updateTime = Time.time;
        if (Crafting.CanStartMerging(animal))
        {
            gameObject.GetComponent<Image>().materialForRendering.SetFloat("_GrayscaleAmount", 0);
        }
        else
        {
            gameObject.GetComponent<Image>().materialForRendering.SetFloat("_GrayscaleAmount", 1);
        }
    }
}
