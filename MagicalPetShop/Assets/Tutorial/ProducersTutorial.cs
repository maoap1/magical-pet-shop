using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Producers Tutorial", menuName = "Tutorials/Producers Tutorial")]
public class ProducersTutorial : Tutorial
{
    private int progress;
    private long updateTime;
    public override bool finished()
    {
        if (progress == 5)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress == 5;
    }

    public override bool tryStart()
    {
        if (SceneManager.GetActiveScene().name == "LabTutorial")
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
            updateTime = Utils.EpochTime();
            canvas.DisableAll();
            canvas.lowerText.Display("You need essences to craft animals and they are produced by collectors.");
            progress++;
        }
        else if (progress == 1 && Utils.EpochTime() - updateTime > 2000)
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
        else if (progress == 2 && GameLogic.THIS.essenceProducerOpened != null && GameLogic.THIS.essenceProducerOpened.essenceAmount.essence.name=="Water")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Navbar/Layout/LabButton").GetComponent<TutorialPanel>());
            canvas.lowerText.Display("Tap on the upgrade button to purchese a new level of the collector!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 715;
            tp.top = 1045;
            tp.width = 300;
            tp.height = 150;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 3 && PlayerState.THIS.producers.Find(p => p.essenceAmount.essence.name=="Water").level == 1)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Display("Congratulations you have just upgraded a collector! You receive 100 coin as a reward!");
            canvas.DisableAll();
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Cauldron").GetComponent<TutorialPanel>());
            progress++;
            updateTime = Utils.EpochTime();
        }
        else if (progress == 4 && Utils.EpochTime() - updateTime > 2000)
        {
            Inventory.AddToInventory(100);
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Close();
            progress++;  
        }
    }
}
