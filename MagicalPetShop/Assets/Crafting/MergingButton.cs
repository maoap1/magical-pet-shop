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
            this.recipes.gameObject.SetActive(false);
            this.merging.gameObject.SetActive(true);
            this.merging.defaultMergingCategory.Display();
            this.rectComponent.sizeDelta = new Vector2(110, 110);

            this.imageComponent.sprite = mergingSprite;
        } else {
            this.isCrafting = true;
            this.merging.gameObject.SetActive(false);
            GameLogic.THIS.inCrafting = true;
            this.recipes.gameObject.SetActive(true);
            this.recipes.defaultRecipeCategory.Display();
            this.imageComponent.sprite = craftingSprite;
            this.rectComponent.sizeDelta = new Vector2(185, 185);
        }
        FindObjectOfType<AudioManager>().Play(SoundType.TabSwitch);
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
