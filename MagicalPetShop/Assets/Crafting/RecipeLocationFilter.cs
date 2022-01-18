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

    private EssenceAmount essenceAmount = null;
    private bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        artwork.sprite = GameGraphics.THIS.unknown;
    }

    public void Update() {
        UpdateUnlocked();
        if (unlocked) {
            if (selected) {
                artwork.sprite = activeSprite;
            } else {
                artwork.sprite = inactiveSprite;
            }
        } else {
            if (selected) {
                artwork.sprite = GameGraphics.THIS.unknownHighlight;
            } else {
                artwork.sprite = GameGraphics.THIS.unknown;
            }
        }
    }

    public void Display()
    {
        UpdateUnlocked();
        foreach (Transform child in recipesDisplayPanel.transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (RecipeLocationFilter rlf in locationFilterPanel.GetComponentsInChildren<RecipeLocationFilter>()) {
            if (rlf.selected)
            {
                foreach (SlidingTween tw in rlf.gameObject.GetComponentsInChildren<SlidingTween>())
                {
                    tw.SlideYBackCurve();
                }
            }
            rlf.selected = false;
            rlf.UpdateNew();
        }
        selected = true;
        foreach (SlidingTween tw in this.gameObject.GetComponentsInChildren<SlidingTween>())
        {
            tw.SlideY();
        }
        recipesPanel.defaultRecipeCategory = this;


        List<RecipeProgress> recipesDisplay = PlayerState.THIS.recipes.FindAll(r => (r.recipe.animal.category == locationType));
        recipesDisplay.Sort((r1, r2) => r2.recipe.animal.level.CompareTo(r1.recipe.animal.level));
        recipesDisplayPanel.Display(recipesDisplay);
        GameLogic.THIS.currentRecipeCategory = locationType;
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

    private void UpdateUnlocked() {
        if (essenceAmount == null && PlayerState.THIS.resources != null && PlayerState.THIS.resources.Count > 0) {
            foreach (EssenceAmount r in PlayerState.THIS.resources) {
                if (r.essence.essenceName == locationType.name) {
                    essenceAmount = r;
                }
            }
        } else if (essenceAmount != null) {
            if (essenceAmount.unlocked & !unlocked) {
                unlocked = true;
            }
        }
    }
}
