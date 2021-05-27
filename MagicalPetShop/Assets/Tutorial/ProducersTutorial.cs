using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Producers Tutorial", menuName = "Tutorials/Producers Tutorial")]
public class ProducersTutorial : Tutorial
{
    private long updateTime;
    public override bool finished()
    {
        if (progress == 7)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress == 7;
    }

    public override void startWithProgress(int progress)
    {
        if (progress < 4)
        {
            this.progress = 0;
        }
        else if (progress < 7)
        {
            this.progress = 4;
        }
    }

    public override bool tryStart()
    {
        if (SceneManager.GetActiveScene().name == "Lab")
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
            canvas.DisableAll();
            canvas.lowerText.Display("You need essences to craft animals and they are produced by collectors.");
            progress++;
        }
        else if (progress == 2 && Utils.EpochTime() - updateTime > 3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("Tap on the water collector to increase its capacity and production rate!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 335;
            tp.top = 150;
            tp.width = 160;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 3 && GameLogic.THIS.essenceProducerOpened != null && GameLogic.THIS.essenceProducerOpened.essenceAmount.essence.name=="Water")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Navbar/Layout/LabButton").GetComponent<TutorialPanel>());
            canvas.lowerText.Display("Tap on the upgrade button to purchese a new level of the collector!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 600;
            tp.top = 1055;
            tp.width = 400;
            tp.height = 150;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 4 && PlayerState.THIS.producers.Find(p => p.essenceAmount.essence.name=="Water").level == 1)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("Congratulations you have just upgraded a collector!");
            canvas.DisableAll();
            progress++;
            updateTime = Utils.EpochTime();
        }
        else if (progress == 5 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("You receive 100 coins as a reward!");
            canvas.DisableAll();
            progress++;
            updateTime = Utils.EpochTime();
        }
        else if (progress == 6 && Utils.EpochTime() - updateTime > 2000)
        {
            Inventory.AddToInventory(100);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Close();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;  
        }
    }
}
