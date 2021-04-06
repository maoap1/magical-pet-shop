using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationAnimalSlotUI : MonoBehaviour
{

    AnimalsUI animalsUI;
    PackOverviewUI packOverviewUI;
    Animal animal;
    Pack pack;
    int slotIndex;

    public void Initialize(AnimalsUI animalsUI, PackOverviewUI packOverviewUI, Animal animal, Pack pack, int slotIndex) {
        this.animalsUI = animalsUI;
        this.packOverviewUI = packOverviewUI;
        this.animal = animal;
        this.pack = pack;
        this.slotIndex = slotIndex;
    }

    public void Clicked() {
        PacksManager.AssignAnimal(this.animal, this.pack, this.slotIndex);
        this.animalsUI.Close();
        this.packOverviewUI.Open(this.pack);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
