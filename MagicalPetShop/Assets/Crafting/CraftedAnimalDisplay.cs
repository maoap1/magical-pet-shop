using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Analytics;

public class CraftedAnimalDisplay : MonoBehaviour {
    public CraftedAnimal craftedAnimal;
    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image animalImage;
    public Image imageMask;
    public int ghostIndex;
    private bool finished;
    private bool claimed;
    private Color defaultBgColor;

    public void Start()
    {
        defaultBgColor = UIPalette.THIS.GetColor(imageMask.gameObject.GetComponent<ImageColor>().color);
        imageMask.color = this.defaultBgColor;
        readyMessage.SetActive(true);
        progressRing.gameObject.SetActive(true);
        finished = false;
        claimed = false;
        animalImage.sprite = craftedAnimal.animal.artwork;
        if (!PlayerState.THIS.crafting.Contains(craftedAnimal))
        {
            PlayerState.THIS.crafting.Add(craftedAnimal);
        }
    }
    public void Update()
    {
        if (!finished)
        {
            if (craftedAnimal.fillRate >= 1)
            {
                finished = true;
                progressRing.gameObject.SetActive(false);
                readyMessage.GetComponentInChildren<TextMeshProUGUI>().text = "Ready";
                GetComponentInParent<FlashingTweenController>().AddNew(readyMessage.transform.GetChild(0));
                imageMask.color = UIPalette.THIS.GetColor(PaletteColor.GridItem);
            }
            else
            {
                readyMessage.GetComponentInChildren<TextMeshProUGUI>().text = craftedAnimal.RemainingSeconds.FormattedTime();
                progressRing.fillRate = craftedAnimal.fillRate;
                imageMask.color = this.defaultBgColor;
            }
        }
    }

    public void OnPointerClicked()
    {
        if (finished && !claimed) {
            claimed = true;
            GetComponent<TweenAnimalToInventory>().Tween(craftedAnimal.animal);
            animalImage.gameObject.SetActive(false);
            InventoryAnimal ia = new InventoryAnimal();
            ia.animal = craftedAnimal.animal;
            ia.count = 1;
            ia.rarity = craftedAnimal.rarity;
            Inventory.AddToInventory(ia);
            PlayerState.THIS.crafting.Remove(craftedAnimal);
            PlayerState.THIS.Save();
            if (craftedAnimal.isRecipe) {
                Analytics.LogEvent("crafting_ended", new Parameter("animal", craftedAnimal.animal.name), new Parameter("rarity", craftedAnimal.rarity.ToString()));
            } else {
                Analytics.LogEvent("merging_ended", new Parameter("animal", craftedAnimal.animal.name), new Parameter("rarity", craftedAnimal.rarity.ToString()));
            }
            if (craftedAnimal.isUpgraded) {
                HigherRarityCrafted newRecipeDisplay = Resources.FindObjectsOfTypeAll<HigherRarityCrafted>()[0];
                newRecipeDisplay.Open(PlayerState.THIS.recipes.Find(r => r.animal == craftedAnimal.animal), craftedAnimal.rarity);
            } else if (craftedAnimal.isRecipe) {
                PlayerState.THIS.recipes.Find(r => r.animal == craftedAnimal.animal).animalProduced();
            }
            FindObjectOfType<AudioManager>().Play(SoundType.Crafting);
            GetComponent<AlphaTween>().FadeOut();
            GameObject.FindObjectOfType<CraftingInfo>().RemoveAnimal(this);
        } else {
            FindObjectOfType<AudioManager>().Play(SoundType.InactiveButton);
        }
    }
}
