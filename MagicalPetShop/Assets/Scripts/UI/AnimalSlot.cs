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

    public void SetAnimal(InventoryAnimal animal) {
        this.name.text = animal.animal.name;
        this.value.text = animal.animal.value.ToString();
        this.image.sprite = animal.animal.artwork;
        this.count.text = animal.count.ToString();
    }
}
