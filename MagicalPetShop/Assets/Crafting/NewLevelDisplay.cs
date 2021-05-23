using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class NewLevelDisplay : MonoBehaviour {
    public void Open()
    {
        GetComponent<AppearHideComponent>().Do();
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
    }

    public void Close()
    {
        int money = GameLogic.THIS.moneyForLevels[PlayerState.THIS.level - 2];
        Inventory.AddToInventory(money);
        GetComponent<AppearHideComponent>().Revert();
    }
}
