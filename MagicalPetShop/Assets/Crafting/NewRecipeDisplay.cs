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
        this.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
        newLevelDisplay = Resources.FindObjectsOfTypeAll<NewLevelDisplay>()[0];
    }

    public void Close() {
        this.gameObject.SetActive(false);
        GetComponent<AppearHideComponent>().Revert();
        if (level)
        {
            newLevelDisplay.Open();
        }
    }
}
