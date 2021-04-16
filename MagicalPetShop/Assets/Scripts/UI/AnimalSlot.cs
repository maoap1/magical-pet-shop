using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSlot : MonoBehaviour
{
    [SerializeField]
    private Text name;
    [SerializeField]
    private Text value;
    [SerializeField]
    public Image image;
    [SerializeField]
    private Text count;
    [SerializeField]
    private Text quality;

    public void SetAnimal(InventoryAnimal animal) {
        this.name.text = animal.animal.name;
        this.value.text = ((int)(animal.animal.value * GameLogic.THIS.getRarityMultiplier(animal.rarity))).ToString();
        this.image.sprite = animal.animal.artwork;
        this.count.text = animal.count.ToString();
        this.quality.text = animal.rarity.ToString("G");
    }
}
