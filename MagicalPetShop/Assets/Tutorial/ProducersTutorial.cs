using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Producers Tutorial", menuName = "Tutorials/Producers Tutorial")]
public class ProducersTutorial : Tutorial
{
    private long updateTime;
    private bool completed = false;
    public override bool finished()
    {
        if (progress == 7 && !completed)
        {
            Tutorials.THIS.settingsDisabled = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
            completed = true;
        }
        return progress == 7;
    }

    public override void startWithProgress(int progress)
    {
        Tutorials.THIS.settingsDisabled = true;
        completed = false;
        if (SceneManager.GetActiveScene().name != "Lab")
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            this.progress = 0;
        }
        else if (progress < 5)
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            this.progress = 0;
        }
        else if (progress < 7)
        {
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            this.progress = 4;
        }
        if (progress == 7)
        {
            this.progress = 7;
        }
    }

    public override bool tryStart()
    {
        if (SceneManager.GetActiveScene().name == "Lab" && PlayerState.THIS.money >= PlayerState.THIS.producers[0].model.GetCost(1))
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
        if (progress == 0 && SceneManager.GetActiveScene().name == "Lab")
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
            canvas.DisableAll();
            canvas.middleText.Display("You need essences to craft animals and they are produced by collectors.");
            progress++;
        }
        else if (progress == 2 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.middleText.Display("Tap on the water collector to increase its capacity and production rate!");
            Rect tp = new Rect
            {
                x = 335,
                y = 150,
                width = 160,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 3 && GameLogic.THIS.essenceProducerOpened != null &&
            GameLogic.THIS.essenceProducerOpened.essenceAmount != null &&
            GameLogic.THIS.essenceProducerOpened.essenceAmount.essence != null && 
            GameLogic.THIS.essenceProducerOpened.essenceAmount.essence.name=="Water")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.middleText.Close();
            canvas.upperText.Display("Tap on the upgrade button to purchase a new level of the collector!");
            Rect tp = new Rect
            {
                x = 600,
                y = 1055,
                width = 400,
                height = 150
            };
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 4 && PlayerState.THIS.producers.Find(p => p.essenceAmount.essence.name=="Water").level == 1)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Congratulations you have just upgraded a collector!");
            canvas.DisableAll();
            progress++;
            PlayerState.THIS.Save();
            updateTime = Utils.EpochTime();
        }
        else if (progress == 5 && Utils.ClickOrTouchEnd())
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You receive 100 coins as a reward!");
            canvas.DisableAll();
            progress++;
            updateTime = Utils.EpochTime();
        }
        else if (progress == 6 && Utils.ClickOrTouchEnd())
        {
            Inventory.AddToInventory(100);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.middleText.Close();
            canvas.upperText.Close();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
            PlayerState.THIS.Save();
        }
    }
}
