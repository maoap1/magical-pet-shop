using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MergingLocationFilter : MonoBehaviour
{
    public MergingSelection mergingPanel;
    public LocationType locationType;
    public Image artwork;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public MergingDisplayPanel mergingDisplayPanel;
    public GameObject locationFilterPanel;
    [HideInInspector]
    public bool selected = false;

    private EssenceAmount essenceAmount = null;
    private bool unlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        artwork.sprite = GameGraphics.THIS.unknown;
    }

    public void Update()
    {
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

    public void Display() {
        UpdateUnlocked();
        foreach (Transform child in mergingDisplayPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<InventoryAnimal> animalsDisplay = PlayerState.THIS.animals.FindAll(a => (a.animal.category == locationType && a.count>1 && a.rarity<Rarity.Legendary));
        animalsDisplay.OrderByDescending(a=>a.animal.level).ThenByDescending(a=>a.rarity);
        mergingDisplayPanel.Display(animalsDisplay);
        foreach (MergingLocationFilter mlf in locationFilterPanel.GetComponentsInChildren<MergingLocationFilter>())
        {
            mlf.selected = false;
        }
        selected = true;
        mergingPanel.defaultMergingCategory = this;
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
