using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AppearHideComponent))]
public class NewLevelDisplay : MonoBehaviour {
    public void Open()
    {
        GameLogic.THIS.inNewLevelDisplay = true;
        GetComponent<AppearHideComponent>().Do();
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
    }

    public void Close()
    {
        GameLogic.THIS.inNewLevelDisplay = false;
        int money = GameLogic.THIS.moneyForLevels[PlayerState.THIS.level - 2];
        Inventory.AddToInventory(money);
        FindObjectOfType<AudioManager>().Play(SoundType.Cash);
        GetComponent<AppearHideComponent>().Revert();
    }
}
