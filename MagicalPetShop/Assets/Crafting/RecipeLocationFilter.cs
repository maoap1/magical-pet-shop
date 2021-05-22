using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLocationFilter : MonoBehaviour
{
    public RecipeSelection recipesPanel;
    public LocationType locationType;
    public Image artwork;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public RecipeDisplayPanel recipesDisplayPanel;
    public GameObject locationFilterPanel;
    public GameObject newRecipe;
    [HideInInspector]
    public bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        artwork.sprite = inactiveSprite;
    }

    public void Update()
    {
        if (selected)
        {
            artwork.sprite = activeSprite;
        }
        else
        {
            artwork.sprite = inactiveSprite;
        }
    }

    public void Display()
    {
        foreach (Transform child in recipesDisplayPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (RecipeLocationFilter rlf in locationFilterPanel.GetComponentsInChildren<RecipeLocationFilter>())
        {
            rlf.selected = false;
            rlf.UpdateNew();
        }
        selected = true;
        recipesPanel.defaultRecipeCategory = this;


        List<RecipeProgress> recipesDisplay = PlayerState.THIS.recipes.FindAll(r => (r.recipe.animal.category == locationType));
        recipesDisplay.Sort((r1, r2) => r2.recipe.animal.level.CompareTo(r1.recipe.animal.level));
        recipesDisplayPanel.Display(recipesDisplay);

    }

    public void UpdateNew()
    {
        if (PlayerState.THIS.recipes.Find(rp => rp.newRecipe && rp.animal.category == locationType) != null)
        {
            newRecipe.SetActive(true);
        }
        else
        {
            newRecipe.SetActive(false);
        }
    }
}
