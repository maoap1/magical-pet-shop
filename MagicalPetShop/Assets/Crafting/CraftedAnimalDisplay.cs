using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;


public class CraftedAnimalDisplay : MonoBehaviour, IPointerDownHandler {
    public CraftedAnimal craftedAnimal;
    public ProgressBar progressRing;
    public GameObject readyMessage;
    public Image animalImage;
    public Image imageMask;
    private bool finished;
    private Color defaultBgColor;
    private Tween tween = null;

    public void Start()
    {
        defaultBgColor = UIPalette.THIS.GetColor(imageMask.gameObject.GetComponent<ImageColor>().color);
        imageMask.color = this.defaultBgColor;
        readyMessage.SetActive(true);
        progressRing.gameObject.SetActive(true);
        finished = false;
        animalImage.sprite = craftedAnimal.animal.artwork;
        if (!PlayerState.THIS.crafting.Contains(craftedAnimal))
        {
            PlayerState.THIS.crafting.Add(craftedAnimal);
        }
        Update();
    }
    public void Update()
    {
        if (!finished)
        {
            if (craftedAnimal.fillRate >= 1)
            {
                finished = true;
                progressRing.gameObject.SetActive(false);
                readyMessage.SetActive(true);
                imageMask.color = UIPalette.THIS.GetColor(PaletteColor.GridItem);
            }
            else
            {
                readyMessage.SetActive(false);
                progressRing.fillRate = craftedAnimal.fillRate;
                imageMask.color = this.defaultBgColor;
            }
        }
        if (PlayerState.THIS.crafting.Find(x => x == craftedAnimal) == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        SafeKillTween();
    }

    private void SafeKillTween()
    {
        if (tween == null) return;
        if (tween.active) tween.Kill();
    }

    public void OnPointerClicked()
    {
        if (finished) {
            GetComponent<TweenAnimalToInventory>().Tween(craftedAnimal.animal);

            InventoryAnimal ia = new InventoryAnimal();
            ia.animal = craftedAnimal.animal;
            ia.count = 1;
            ia.rarity = craftedAnimal.rarity;
            Inventory.AddToInventory(ia);
            PlayerState.THIS.crafting.Remove(craftedAnimal);
            PlayerState.THIS.Save();
            if (craftedAnimal.isUpgraded) {
                HigherRarityCrafted newRecipeDisplay = Resources.FindObjectsOfTypeAll<HigherRarityCrafted>()[0];
                newRecipeDisplay.Open(PlayerState.THIS.recipes.Find(r => r.animal == craftedAnimal.animal), craftedAnimal.rarity);
            } else if (craftedAnimal.isRecipe) {
                PlayerState.THIS.recipes.Find(r => r.animal == craftedAnimal.animal).animalProduced();
            }
            FindObjectOfType<AudioManager>().Play(SoundType.Crafting);
            SafeKillTween();
            Destroy(this.gameObject);
        } else {
            SafeKillTween();
            tween = gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        SafeKillTween();
        tween = gameObject.transform.DOScale(new Vector3(0.95f, 0.95f, 0.95f), 0.1f);
    }
}
