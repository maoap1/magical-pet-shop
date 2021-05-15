using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used in a summary of expedition results
public class CasualtyIconUI : MonoBehaviour
{
    [SerializeField]
    Image iconImage;

    public void Initialize(InventoryAnimal animal) {
        this.iconImage.material = new Material(this.iconImage.material);
        this.iconImage.sprite = animal.animal.artwork;
        this.iconImage.material.SetColor("_Color", GameGraphics.THIS.getRarityColor(animal.rarity));
        this.iconImage.material.SetTexture("_BloomTex", animal.animal.bloomSprite.texture);
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
