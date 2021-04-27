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
    public MergingDisplayPanel mergingDisplayPanel;
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
}
