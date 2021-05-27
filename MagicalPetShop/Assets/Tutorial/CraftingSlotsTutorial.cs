using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Crafting Slots Tutorial", menuName = "Tutorials/Crafting Slots Tutorial")]
public class CraftingSlotsTutorial : Tutorial
{
    private int progress;
    private long updateTime;
    public override bool finished()
    {
        if (progress == 6)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress == 6;
    }

    public override bool tryStart()
    {
        if ((SceneManager.GetActiveScene().name == "LabTutorial" || SceneManager.GetActiveScene().name == "ShopTutorial") && PlayerState.THIS.craftingSlots == 1 && PlayerState.THIS.crafting.Count==1)
        {
            progress = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void update()
    {
        if (progress == 0)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAll(true, false);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 1 && Utils.EpochTime()-updateTime>1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Having only one crafting slot can make crafting slow.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 2 && Utils.EpochTime()-updateTime>3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Luckily you can buy a new one. Click on the + icon to do so!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 650;
            tp.top = 1495;
            tp.width = 200;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 3 && GameLogic.THIS.buyingCraftingSlot)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Tap on the OK button to buy a new crafting slot");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 545;
            tp.top = 1100;
            tp.width = 250;
            tp.height = 150;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 4 && !GameLogic.THIS.buyingCraftingSlot)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You just bought a new crafting slot. You receive 100 coins as a reward!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 5 && Utils.EpochTime() - updateTime > 2000)
        {
            Inventory.AddToInventory(100);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            progress++;
        }
    }
}