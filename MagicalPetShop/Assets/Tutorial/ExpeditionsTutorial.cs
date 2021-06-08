using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

[CreateAssetMenu(fileName = "Expeditions Tutorial", menuName = "Tutorials/Expeditions Tutorial")]
public class ExpeditionsTutorial : Tutorial
{
    private long updateTime;
    private bool completed = false;
    public override bool finished()
    {
        if (progress == 100 && !completed)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
            completed = true;
        }
        return progress == 100;
    }

    public override void startWithProgress(int progress)
    {
        Tutorials.THIS.settingsDisabled = true;
        completed = false;
        if (progress < 8)
        {
            this.progress = 1;
        }
        else if (progress < 12)
        {
            this.progress = 9;
        }
        else if (progress < 21)
        {
            this.progress = 14;
        }
        else if (progress < 26)
        {
            this.progress = 21;
        }
        else if (progress < 50)
        {
            this.progress = 26;
        }
        else if (progress == 100)
        {
            this.progress = 100;
        }
    }

    public override bool tryStart()
    {
        if (PlayerState.THIS.level == 3)
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
        Debug.Log(progress);
        if (progress == 0 && GameLogic.THIS.inNewLevelDisplay)
        {
            Tutorials.THIS.settingsDisabled = true;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            progress++;
        }
        else if (progress == 1 && !GameLogic.THIS.inNewLevelDisplay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.DisableAll(true, true);
            progress++;
        }
        else if (progress == 2 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("You advanced to the third level. This allows you to use the yard!");
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 3 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("In the yard, you can send animals to expeditions for rare artifacts!");
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 4 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You can use these to craft higher-level animals.");
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 5 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("To send animals on the expedition you have to organize them in a pack.");
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 6 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Move to the pack manager!");
            Rect tp = new Rect
            {
                x = 860,
                y = 1670,
                width = 200,
                height = 250
            };
            updateTime = Utils.EpochTime();
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 7 && GameLogic.THIS.inPackLeaderSelection && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You have to buy someone to lead the pack. Get the money to do so!");
            Rect tp = new Rect
            {
                x = 60,
                y = 330,
                width = 480,
                height = 750
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 8 && (Utils.EpochTime() - updateTime > 3000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.EnableAll();
            Tutorials.THIS.settingsDisabled = false;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
        }
        else if (progress == 9 && PlayerState.THIS.packs[0].owned)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAll(true, true);
            Tutorials.THIS.settingsDisabled = true;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 10 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Now that you have a pack leader you have to assign animals to the pack!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 11 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Each pack slot requires an animal of a certain type!");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 12 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Assign at least 4 animals! If you don't have the required categories craft them!");
            updateTime = Utils.EpochTime();
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 13 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.EnableAll();
            updateTime = Utils.EpochTime();
            Tutorials.THIS.settingsDisabled = false;
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 14 && GameLogic.THIS.inAnimalsUI)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.DisableAll(true, true);
            canvas.SetMask(Vector3.zero, new Vector2(1080, 1920));
            progress++;
            Tutorials.THIS.settingsDisabled = true;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
        }
        else if (progress == 15 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Animals can be assigned to the pack by clicking on them!");
            Rect tp = new Rect
            {
                x = 70,
                y = 405,
                width = 320,
                height = 530
            };
            canvas.Highlight(tp, true);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 16 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Each animal has strength.");
            Rect tp = new Rect
            {
                x = 120,
                y = 805,
                width = 220,
                height = 60
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 17 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Higher strength allows it to succeed in harder expeditions.");
            Rect tp = new Rect
            {
                x = 120,
                y = 805,
                width = 220,
                height = 60
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 18 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("It also has some chance of dying on an expedition.");
            Rect tp = new Rect
            {
                x = 120,
                y = 860,
                width = 220,
                height = 60
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 19 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Higher animal rarity means more strength and less chance dying!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 20 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.EnableAll();
            Tutorials.THIS.settingsDisabled = false;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 21 && PlayerState.THIS.packs[0].slots.Where(s => s.animal != null).Count() >= 4)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.DisableAll(true, true);
            progress++;
        }
        else if (progress == 22 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 23 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Now that you have a pack ready you can send it on an expedition!");
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 24 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd())) {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Close the pack window and move to the yard (if you are not already there)!");
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 25 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.EnableAll();
            Tutorials.THIS.settingsDisabled = false;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 26 && !GameLogic.THIS.inPackLeaderSelection && !GameLogic.THIS.inPackOverview && SceneManager.GetActiveScene().name == "Yard")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.DisableAll(true, true);
            canvas.SetMask(Vector3.zero, new Vector2(1080, 1920));
            progress++;
            Tutorials.THIS.settingsDisabled = true;
            PlayerState.THIS.Save();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
        }
        else if (progress == 27 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Click on the portal to open its expeditions!");
            Rect tp = new Rect
            {
                x = 0,
                y = 1000,
                width = 400,
                height = 600
            };
            canvas.DisableAllExcept(tp, true);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 28 && GameLogic.THIS.inExpeditionList)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Select the first expedition!");
            Rect tp = new Rect
            {
                x = 50,
                y = 400,
                width = 980,
                height = 380
            };
            canvas.DisableAllExcept(tp);
            PlayerState.THIS.lastExpeditionDifficulties[0] = ExpeditionDifficulty.Medium;
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 29 && GameLogic.THIS.inExpedition)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The default expedition difficulty is medium!");
            Rect tp = new Rect
            {
                x = 390,
                y = 540,
                width = 300,
                height = 100
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 30 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Higher difficulty expeditions give you more artifacts!");
            Rect tp = new Rect
            {
                x = 390,
                y = 540,
                width = 300,
                height = 100
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 31 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("In this case, you receive between 8 and 12 artifacts for medium difficulty!");
            Rect tp = new Rect
            {
                x = 290,
                y = 635,
                width = 500,
                height = 100
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 32 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("And the expedition takes 5 min and 0 seconds!");
            Rect tp = new Rect
            {
                x = 290,
                y = 725,
                width = 500,
                height = 100
            };
            canvas.DisableAllExcept(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 33 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Here you can select a pack to send on an expedition!");
            Rect tp = new Rect
            {
                x = 50,
                y = 850,
                width = 980,
                height = 440
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 34 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The face under the pack indicates the chance of expedition success.");
            Rect tp = new Rect
            {
                x = 50,
                y = 850,
                width = 980,
                height = 440
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 35 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The pack can be selected by tapping on it.");
            Rect tp = new Rect
            {
                x = 50,
                y = 850,
                width = 980,
                height = 440
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 36 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You can also hold your finger on the pack to inspect it.");
            Rect tp = new Rect
            {
                x = 50,
                y = 850,
                width = 980,
                height = 440
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 37 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The medium difficulty might be too hard for your first expedition.");
            Rect tp = new Rect
            {
                x = 50,
                y = 850,
                width = 980,
                height = 440
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 38 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("If this is the case you can change the difficulty to easy.");
            Rect tp = new Rect
            {
                x = 85,
                y = 605,
                width = 100,
                height = 150
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 39 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Once you are ready you can start the expedition by clicking on go!");
            Rect tp = new Rect
            {
                x = 610,
                y = 1355,
                width = 400,
                height = 150
            };
            canvas.Highlight(tp);
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress == 40 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("As a reward for completing this tutorial, you receive 1000 coins!");
            canvas.DisableAll();
            updateTime = Utils.EpochTime();
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress == 41 && (Utils.EpochTime() - updateTime > 4000 || Utils.ClickOrTouchEnd()))
        {
            Inventory.AddToInventory(1000);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.EnableAll();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            Tutorials.THIS.settingsDisabled = false;
            progress = 100;
            PlayerState.THIS.Save();
        }
    }
}
