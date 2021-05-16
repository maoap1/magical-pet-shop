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

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;

    public void Open(RecipeProgress rp, bool newLevel)
    {
        level = newLevel;
        animalImage.sprite = rp.animal.artwork;
        animalName.text = rp.animal.name;
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToHide) {
            g.SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
        newLevelDisplay = Resources.FindObjectsOfTypeAll<NewLevelDisplay>()[0];
    }

    public void Close() {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(true);
        }
        if (level)
        {
            newLevelDisplay.Open();
        }
    }
}
