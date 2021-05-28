using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Upgrades Tutorial", menuName = "Tutorials/Upgrades Tutorial")]
public class UpgradesTutorial : Tutorial
{
    private long updateTime;
    public override bool finished()
    {
        if (progress == 28)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress == 28;
    }

    public override void startWithProgress(int progress)
    {
        if (progress < 28)
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            this.progress = 0;
        }
    }

    public override bool tryStart()
    {
        if (GameLogic.THIS.inNewRecipeDisplay)
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
            PlayerState.THIS.Save();
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            canvas.DisableAll(true, false);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 1 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.lowerText.Display("You just unlocked a recipe upgrade.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 2 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.lowerText.Display("In this case you found recipe for a new animal.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 3 && Utils.EpochTime() - updateTime > 3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.lowerText.Close();
            canvas.upperText.Display("Close the anouncement to find out where to craft the unlocked animal.");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 420;
            tp.top = 1455;
            tp.width = 250;
            tp.height = 150;
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
            TutorialPanel tp = new TutorialPanel();
            tp.left = 20;
            tp.top = 1670;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            canvas.lowerText.Close();
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
            canvas.DisableAll(true, false);
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
            TutorialPanel tp = new TutorialPanel();
            tp.left = 90;
            tp.top = 900;
            tp.width = 900;
            tp.height = 700;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 8 && GameLogic.THIS.inCrafting)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.lowerText.Display("A new animal was discovered in the earth category, move there!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 230;
            tp.top = 500;
            tp.width = 150;
            tp.height = 150;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 9 && GameLogic.THIS.currentRecipeCategory.name == "Earth")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.lowerText.Display("Now you know where to craft new animals. Lets find out more about upgrades!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 80;
            tp.top = 500;
            tp.width = 150;
            tp.height = 150;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 10 && GameLogic.THIS.currentRecipeCategory.name == "Water")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("Click on the info to find out more about the recipe!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 72;
            tp.top = 1102;
            tp.width = 60;
            tp.height = 60;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 11 && GameLogic.THIS.inRecipeInfo)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("This display tells you everything important about the veiltail fish!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 50;
            tp.top = 490;
            tp.width = 980;
            tp.height = 1325;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 12 && Utils.EpochTime()-updateTime>2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("The name and a picture of the crafted animal!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 50;
            tp.top = 490;
            tp.width = 980;
            tp.height = 350;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 13 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("Resources needed for crafting.");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 50;
            tp.top = 810;
            tp.width = 980;
            tp.height = 380;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 14 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("The number of animals of this type produced so far.");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 50;
            tp.top = 1150;
            tp.width = 980;
            tp.height = 150;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 15 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("Producing enough animals can give you various upgrades!");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 16 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Close();
            canvas.upperText.Display("You already unlocked the first upgrade - new recipe!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 60;
            tp.top = 1280;
            tp.width = 200;
            tp.height = 300;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 17 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("To do so you needed to produce 7 fish.");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 18 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Other upgrades include lower crafting cost, faster crafting time...");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 50;
            tp.top = 1280;
            tp.width = 980;
            tp.height = 300;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 19 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("...higher selling cost or higher minimal quality");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 20 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The animal has one or more categories");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 60;
            tp.top = 1580;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 21 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Fish is an animal of tier 1.");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 390;
            tp.top = 1580;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 22 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Animals of higher tiers are stronger and more valuable.");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 23 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Fish of common quality is worth 50 coins.");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 570;
            tp.top = 1580;
            tp.width = 240;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 24 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("And it takes 15 seconds to craft it.");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 785;
            tp.top = 1580;
            tp.width = 240;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 25 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Information about a recipe can also be opened from Inventory.");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 26 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("As a reward for completing this tutorial you receive 200 coins!");
            Inventory.AddToInventory(200);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 27 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Close();
            canvas.upperText.Close();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            canvas.EnableAll();
            progress++;
            PlayerState.THIS.Save();
        }
    }
}
