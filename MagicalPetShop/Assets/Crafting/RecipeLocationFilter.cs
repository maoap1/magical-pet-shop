using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLocationFilter : MonoBehaviour
{
    public LocationType locationType;
    public Image artwork;
    public RecipeDisplayPanel recipesDisplayPanel;
    // Start is called before the first frame update
    void Start()
    {
        artwork.sprite = locationType.artwork;
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
    }
}
