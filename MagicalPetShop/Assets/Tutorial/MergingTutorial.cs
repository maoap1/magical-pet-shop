using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Merging Tutorial", menuName = "Tutorials/Merging Tutorial")]
public class MergingTutorial : Tutorial
{
    private long updateTime;
    private bool completed = false;
    public override bool finished()
    {
        if (progress == 11 && !completed)
        {
            PlayerState.THIS.Save();
            Tutorials.THIS.settingsDisabled = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
            completed = true;
        }
        return progress == 11;
    }

    public override void startWithProgress(int progress)
    {
        Tutorials.THIS.settingsDisabled = true;
        completed = false;
        if (progress < 11)
        {
            //SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            //switcher.on = false;
            this.progress = 0;
        }
        if (progress == 11)
        {
            this.progress = 11;
        }
    }

    public override bool tryStart()
    {
        if (SceneManager.GetActiveScene().name == "Lab" && PlayerState.THIS.level >= 3 && PlayerState.THIS.artifacts.Count > 0)
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
        if (progress == 0 && SceneManager.GetActiveScene().name == "Lab" && PlayerState.THIS.level >= 3 && PlayerState.THIS.artifacts.Count > 0)
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
        else if (progress == 1 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("The last important thing to learn about is merging!");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 2 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Using two animals of lower quality to craft one animal of higher rarity.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 3 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("To try this tap on the cauldron!");
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
        else if (progress == 4 && GameLogic.THIS.inCrafting)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.middleText.Display("Change from crafting to merging!");
            Rect tp = new Rect
            {
                x = 160,
                y = 400,
                width = 160,
                height = 160
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 5 && GameLogic.THIS.inMerging)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.middleText.Close();
            canvas.upperText.Display("Merging is started the same way as crafting, just the cost is different!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 6 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You pay two animals of lower quality and some artifacts!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 7 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("When merging finishes you receive an animal of higher rarity!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 8 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Congratulations for completing the tutorial!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 9 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You receive 1000 coins as a reward!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 10 && Utils.ClickOrTouchEnd())
        {
            Inventory.AddToInventory(1000);
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
