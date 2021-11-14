using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Upgrades Tutorial", menuName = "Tutorials/Upgrades Tutorial")]
public class UpgradesTutorial : Tutorial
{
    private long updateTime;
    private bool completed = false;
    public override bool finished()
    {
        if (progress == 29 && !completed)
        {
            Tutorials.THIS.settingsDisabled = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
            completed = true;
        }
        return progress == 29;
    }

    public override void startWithProgress(int progress)
    {
        Tutorials.THIS.settingsDisabled = true;
        completed = false;
        if (progress < 29)
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            this.progress = 0;
        }
        if (progress == 29)
        {
            this.progress = 29;
        }
    }

    public override bool tryStart()
    {
        if (GameLogic.THIS.inNewRecipeDisplay)
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
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            canvas.DisableAll(true, true);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 1 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("You just unlocked a recipe upgrade!");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 2 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("In this case, you found a recipe for a new animal.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 3 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Close the announcement to find out where to craft the unlocked animal.");

            Rect tp = new Rect
            {
                x = 420,
                y = 1455,
                width = 250,
                height = 150
            };
            canvas.DisableAllExcept(tp);
            if (SceneManager.GetActiveScene().name == "Shop")
            {
                progress++;
            }
            else
            {
                progress = 7;
            }
        }
        else if (progress == 4 && !GameLogic.THIS.inNewRecipeDisplay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            Rect tp = new Rect
            {
                x = 20,
                y = 1670,
                width = 200,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            canvas.upperText.Display("Move to the lab!");
            canvas.leftArrow.SetActive(true);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 5 && SceneManager.GetActiveScene().name == "Lab")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            canvas.leftArrow.SetActive(false);
            switcher.on = false;
            canvas.DisableAll(true, true);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 6 && Utils.EpochTime() - updateTime > 1000)
        {
            progress++;
        }
        else if (progress == 7 && SceneManager.GetActiveScene().name == "Lab" && !GameLogic.THIS.inNewRecipeDisplay)
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.upperText.Display("Tap on the cauldron!");
            Rect tp = new Rect
            {
                x = 90,
                y = 900,
                width = 900,
                height = 700
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 8 && GameLogic.THIS.inCrafting)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("A new animal was discovered in the earth category, move there!");
            Rect tp = new Rect
            {
                x = 230,
                y = 500,
                width = 150,
                height = 150
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 9 && GameLogic.THIS.currentRecipeCategory.name == "Earth")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Now you know where to craft new animals. Let's find out more about upgrades!");
            Rect tp = new Rect
            {
                x = 80,
                y = 500,
                width = 150,
                height = 150
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 10 && GameLogic.THIS.currentRecipeCategory.name == "Water")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Click on the info to find out more about the recipe!");
            Rect tp = new Rect
            {
                x = 72,
                y = 1102,
                width = 60,
                height = 60
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 11 && GameLogic.THIS.inRecipeInfo)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("This display tells you everything important about the veiltail fish!");
            Rect tp = new Rect
            {
                x = 50,
                y = 490,
                width = 980,
                height = 1325
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 12 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The name and a picture of the crafted animal!");
            Rect tp = new Rect
            {
                x = 50,
                y = 490,
                width = 980,
                height = 350
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 13 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Resources needed for crafting.");
            Rect tp = new Rect
            {
                x = 50,
                y = 810,
                width = 980,
                height = 380
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 14 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The number of animals of this type produced so far.");
            Rect tp = new Rect
            {
                x = 50,
                y = 1150,
                width = 980,
                height = 150
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 15 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Producing enough animals can give you various upgrades!");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 16 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You already unlocked the first upgrade - a new recipe!");
            Rect tp = new Rect
            {
                x = 60,
                y = 1280,
                width = 200,
                height = 300
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 17 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("To do so you had to produce 7 fish.");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 18 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Other upgrades include lower crafting cost, faster crafting time...");
            Rect tp = new Rect
            {
                x = 50,
                y = 1280,
                width = 980,
                height = 300
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 19 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("...higher selling cost or higher minimal quality");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 20 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The animal has one or more categories.");
            Rect tp = new Rect
            {
                x = 60,
                y = 1580,
                width = 200,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 21 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Fish is an animal of tier 1.");
            Rect tp = new Rect
            {
                x = 390,
                y = 1580,
                width = 200,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 22 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Animals of higher tiers are stronger and more valuable.");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 23 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Discovering higher tier animals also increases your level.");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 24 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Fish of the common quality is worth 50 coins.");
            Rect tp = new Rect
            {
                x = 570,
                y = 1580,
                width = 240,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 25 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("And it takes 15 seconds to craft it.");
            Rect tp = new Rect
            {
                x = 785,
                y = 1580,
                width = 240,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 26 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Information about a recipe can also be opened from Inventory.");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 27 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("As a reward for completing this tutorial, you receive 100 coins!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 28 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Close();
            canvas.upperText.Close();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            Inventory.AddToInventory(100);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            switcher.on = true;
            canvas.EnableAll();
            progress++;
            PlayerState.THIS.Save();
        }
    }
}
