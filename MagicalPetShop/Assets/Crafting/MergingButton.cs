using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergingButton : MonoBehaviour
{

    public Sprite craftingSprite;
    public Sprite mergingSprite;
    public RecipeSelection recipes;
    public MergingSelection merging;
    public GameObject image;

    private bool isCrafting = true;
    private Image imageComponent;
    private RectTransform rectComponent;

    public void Toggle() {
        if (this.isCrafting) {
            this.isCrafting = false;
            GameLogic.THIS.inCrafting = false;
            recipes.gameObject.SetActive(false);
            merging.gameObject.TweenAwareEnable();
            this.merging.defaultMergingCategory.Display();
            this.rectComponent.sizeDelta = new Vector2(110, 110);
            GameLogic.THIS.inMerging = true;

            this.imageComponent.sprite = mergingSprite;
        } else {
            this.isCrafting = true;
            recipes.gameObject.SetActive(true);
            GameLogic.THIS.inCrafting = true;
            merging.gameObject.SetActive(false);
            this.recipes.defaultRecipeCategory.Display();
            this.imageComponent.sprite = craftingSprite;
            this.rectComponent.sizeDelta = new Vector2(185, 185);
            GameLogic.THIS.inMerging = false;
        }
    }

    public void Reset() {
        if (this.imageComponent == null) Initialize();
        this.isCrafting = true;
        this.imageComponent.sprite = craftingSprite;
        this.rectComponent.sizeDelta = new Vector2(185, 185);
    }

    private void Initialize() {
        this.imageComponent = this.image.GetComponent<Image>();
        this.imageComponent.sprite = craftingSprite;
        this.rectComponent = this.image.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
}
