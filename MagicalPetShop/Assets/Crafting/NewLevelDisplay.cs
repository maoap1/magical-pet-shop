using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class NewLevelDisplay : MonoBehaviour {
    public void Open()
    {
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear) {
            g.SetActive(true);
        }
        this.gameObject.SetActive(true);
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide) {
            g.SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
    }

    public void Close()
    {
        int money = GameLogic.THIS.moneyForLevels[PlayerState.THIS.level - 2];
        Inventory.AddToInventory(money);
        this.gameObject.SetActive(false);
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToAppear) {
            g.SetActive(false);
        }
        foreach (GameObject g in GetComponent<AppearHideComponent>().ObjectsToHide) {
            g.SetActive(true);
        }
    }
}
