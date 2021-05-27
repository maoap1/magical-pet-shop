using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Expeditions Tutorial", menuName = "Tutorials/Expeditions Tutorial")]
public class ExpeditionsTutorial : Tutorial
{
    private long updateTime;
    public override bool finished()
    {
        if (progress == 60)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress == 60;
    }

    public override void startWithProgress(int progress)
    {
        if (progress<8)
        {
            this.progress = 1;
        }
        else if (progress==8)
        {
            this.progress = 8;
        }
        else
        {
            this.progress = 9;
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
        if (progress == 0 && GameLogic.THIS.inNewLevelDisplay)
        {
            progress++;
        }
        else if (progress == 1 && !GameLogic.THIS.inNewLevelDisplay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.DisableAll(true, false);
            progress++;
        }
        else if (progress == 2 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("You advanced to the third level. This allows you to use the yard!");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 3 && Utils.EpochTime() - updateTime > 3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("In the yard you can send animals to expeditions for rare artifacts!");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 4 && Utils.EpochTime() - updateTime > 3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You can use these to craft higher level animals.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 5 && Utils.EpochTime() - updateTime > 3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("To send animlas on the expedition you have to organize them in a pack.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 6 && Utils.EpochTime() - updateTime > 3000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Move to the pack manager!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 860;
            tp.top = 1670;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 7 && GameLogic.THIS.inPackLeaderSelection)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("You have to buy someone to lead the pack. Get the money to do so!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 60;
            tp.top = 330;
            tp.width = 480;
            tp.height = 650;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress == 8)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.EnableAll();
            progress++;
        }
        else if (progress == 9 && PlayerState.THIS.packs[0].owned)
        {
            Debug.Log("pack leader owned");
        }
    }
}
