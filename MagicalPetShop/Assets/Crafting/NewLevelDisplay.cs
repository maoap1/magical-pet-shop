using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelDisplay : MonoBehaviour
{
    public void Open()
    {
        this.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play(SoundType.Success);
    }

    public void Close()
    {
        int money = GameLogic.THIS.moneyForLevels[PlayerState.THIS.level - 2];
        Inventory.AddToInventory(money);
        this.gameObject.SetActive(false);
    }
}
