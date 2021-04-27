using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRecipeDisplay : MonoBehaviour
{
    public Image animalImage;
    public void Open(RecipeProgress rp)
    {
        animalImage.sprite = rp.animal.artwork;
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
