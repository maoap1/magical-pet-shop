using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLocationFilter : MonoBehaviour
{
    public RecipeSelection recipesPanel;
    public LocationType locationType;
    public Image artwork;
    public RecipeDisplayPanel recipesDisplayPanel;
    public GameObject locationFilterPanel;
    [HideInInspector]
    public bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        artwork.sprite = locationType.artwork;
    }

    public void Update()
    {
        if (selected)
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.gray;
        }
    }

    public void Display()
    {
        foreach (Transform child in recipesDisplayPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<RecipeProgress> recipesDisplay = PlayerState.THIS.recipes.FindAll(r => (r.recipe.animal.category == locationType));
        recipesDisplay.Sort((r1, r2) => r2.recipe.animal.level.CompareTo(r1.recipe.animal.level));
        recipesDisplayPanel.Display(recipesDisplay);
        foreach (RecipeLocationFilter rlf in locationFilterPanel.GetComponentsInChildren<RecipeLocationFilter>())
        {
            rlf.selected = false;
        }
        selected = true;
        recipesPanel.defaultRecipeCategory = this;
    }
}
