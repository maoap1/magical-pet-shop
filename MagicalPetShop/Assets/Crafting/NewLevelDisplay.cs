using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelDisplay : MonoBehaviour {

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;

    public void Open()
    {
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(true);
        }
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToHide) {
            g.SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
    }

    public void Close()
    {
        int money = GameLogic.THIS.moneyForLevels[PlayerState.THIS.level - 2];
        Inventory.AddToInventory(money);
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear) {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide) {
            g.SetActive(true);
        }
    }
}
