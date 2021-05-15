using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRecipeDisplay : MonoBehaviour
{
    public Image animalImage;
    public TMPro.TextMeshProUGUI animalName;
    private bool level = false;
    private NewLevelDisplay newLevelDisplay;
    public void Open(RecipeProgress rp, bool newLevel)
    {
        level = newLevel;
        animalImage.sprite = rp.animal.artwork;
        animalName.text = rp.animal.name;
        this.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
        newLevelDisplay = Resources.FindObjectsOfTypeAll<NewLevelDisplay>()[0];
    }

    public void Close()
    {
        if (level)
        {
            newLevelDisplay.Open();
        }
        this.gameObject.SetActive(false);
    }
}
