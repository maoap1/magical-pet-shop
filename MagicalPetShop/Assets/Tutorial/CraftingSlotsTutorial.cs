using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Crafting Slots Tutorial", menuName = "Tutorials/Crafting Slots Tutorial")]
public class CraftingSlotsTutorial : Tutorial
{
    private long updateTime;
    private bool completed = false;
    public override bool finished()
    {
        if (progress == 6 && !completed)
        {
            PlayerState.THIS.Save();
            Tutorials.THIS.settingsDisabled = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
            completed = true;
        }
        return progress == 6;
    }

    public override void startWithProgress(int progress)
    {
        Tutorials.THIS.settingsDisabled = true;
        completed = false;
        if (progress < 5)
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            this.progress = 0;
        }
        else if (progress < 6)
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            this.progress = 4;
        }
        if (progress == 6)
        {
            this.progress = 6;
        }
    }

    public override bool tryStart()
    {
        if ((SceneManager.GetActiveScene().name == "Lab" || SceneManager.GetActiveScene().name == "Shop") && PlayerState.THIS.craftingSlots == 1 && PlayerState.THIS.crafting.Count==1 && PlayerState.THIS.money >= GameLogic.THIS.craftingSlotUpgrades[0].cost)
        {
            progress = 0;
            completed = false;
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
            Tutorials.THIS.settingsDisabled = true;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAll(true, true);
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
        else if (progress == 2 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Luckily you can buy a new one. Click on the + icon to do so!");
            Rect tp = new Rect
            {
                x = 650,
                y = 1495,
                width = 200,
                height = 200
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 3 && GameLogic.THIS.buyingCraftingSlot)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Tap on the OK button to buy a new crafting slot!");
            Rect tp = new Rect
            {
                x = 545,
                y = 1155,
                width = 300,
                height = 150
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 4 && !GameLogic.THIS.buyingCraftingSlot)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You just bought a new crafting slot. You receive 50 coins as a reward!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 5 && Utils.ClickOrTouchEnd())
        {
            Inventory.AddToInventory(50);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
            PlayerState.THIS.Save();
        }
    }
}