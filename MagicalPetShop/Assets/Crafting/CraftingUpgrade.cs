using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUpgrade : MonoBehaviour
{
    public CraftingInfo craftingInfo;
    public CraftingUpgradeDisplay craftingUpgradeDisplay;

    public void Click()
    {
        craftingUpgradeDisplay.Open(craftingInfo);
    }
}
