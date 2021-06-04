using System.Collections.Generic;
using UnityEngine;

public class CraftingInfo : MonoBehaviour
{
    public GameObject craftedAnimalPrefab;
    public GhostLayoutGroup ghostLayout;
    public CraftingUpgrade buyCraftingSlotObject;

    public List<CraftedAnimalDisplay> displays;

    private bool showBuySlotButton;

    public void AddAnimal(CraftedAnimal craftedAnimal)
    {
        AddAnimalImpl(craftedAnimal);
        SortAndTeleport();
    }

    private void SortAndTeleport()
    {
        displays.Sort((a, b) => a.craftedAnimal.RemainingSeconds
                                .CompareTo(b.craftedAnimal.RemainingSeconds));

        for (int i = 0; i < displays.Count; i++)
        {
            var displ = displays[i];
            displ.ghostIndex = i;
            var follower = displ.GetComponent<GhostPositionFollower>();
            follower.Target = ghostLayout.Positions[i];
            follower.TeleportToTarget();
        }

        int last = PlayerState.THIS.crafting.Count;
        if (last == 5) last = 4;
        var follow = buyCraftingSlotObject.GetComponent<GhostPositionFollower>();
        follow.Target = ghostLayout.Positions[last];
        follow.TeleportToTarget();
    }

    private bool CanShowBuySlotButton()
    {
        return PlayerState.THIS.craftingSlots < 5
            && PlayerState.THIS.crafting.Count == PlayerState.THIS.craftingSlots
            && GameLogic.THIS.craftingSlotUpgrades[PlayerState.THIS.craftingSlots - 1].level <= PlayerState.THIS.level;
    }

    private void AddAnimalImpl(CraftedAnimal craftedAnimal)
    {
        CraftedAnimalDisplay display = Instantiate(craftedAnimalPrefab, this.transform).GetComponent<CraftedAnimalDisplay>();
        display.craftedAnimal = craftedAnimal;
        displays.Add(display);
    }

    public void RemoveAnimal(CraftedAnimalDisplay display)
    {
        displays.Remove(display);
        int removedIndex = display.ghostIndex;
        foreach (var d in displays)
        {
            if (d.ghostIndex > removedIndex)
            {
                d.ghostIndex -= 1;
                d.GetComponent<GhostPositionFollower>().Target = ghostLayout.Positions[d.ghostIndex];
            }
        }

        
    }

    public void Start()
    {
        displays = new List<CraftedAnimalDisplay>();
        foreach (CraftedAnimal craftedAnimal in PlayerState.THIS.crafting)
        {
            AddAnimalImpl(craftedAnimal);
        }

        Invoke(nameof(SortAndTeleport), 0.05f);
    }


    public void Update()
    {
        if (CanShowBuySlotButton())
        {
            if (!showBuySlotButton)
            {
                buyCraftingSlotObject.gameObject.TweenAwareEnable();
                showBuySlotButton = true;
            }
        }
        else
        {
            if (showBuySlotButton)
            {
                buyCraftingSlotObject.gameObject.TweenAwareDisable();
                showBuySlotButton = false;
            }
            
        }
        
        int last = PlayerState.THIS.crafting.Count;
        if (last == 5) last = 4;
        buyCraftingSlotObject.GetComponent<GhostPositionFollower>().Target = ghostLayout.Positions[last];
    }

}
