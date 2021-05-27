using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AppearHideComponent))]
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
        GetComponent<AppearHideComponent>().Do();
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
        newLevelDisplay = Resources.FindObjectsOfTypeAll<NewLevelDisplay>()[0];
        GameLogic.THIS.inNewRecipeDisplay = true;
    }

    public void Close() {
        GetComponent<AppearHideComponent>().Revert();
        GameLogic.THIS.inNewRecipeDisplay = false;
        if (level)
        {
            newLevelDisplay.Open();
        }
    }
}
