using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Levels Tutorial", menuName = "Tutorials/Levels Tutorial")]
public class NewLevelTutorial : Tutorial
{
    private long updateTime;
    public override bool finished()
    {
        if (progress == 11)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress == 11;
    }

    public override void startWithProgress(int progress)
    {
        if (progress < 11)
        {
            this.progress = 0;
        }
    }

    public override bool tryStart()
    {
        if (PlayerState.THIS.level == 2 && GameLogic.THIS.inNewLevelDisplay)
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
            canvas.upperText.Display("You just advanced to a new level!");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 2 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("This is because you discovered a recipe for a tier 2 animal.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 3 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Tier 2 animals are more valuable stronger and take longer to craft.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 4 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Once you reach level 3 you will gain access to a yard.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 5 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("There you can send your animals to dangerous expeditions.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 6 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("Animals can bring artifacts from them.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 7 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("These are used to craft more powerful animals.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 8 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("There is also money reward for advancing to the next level.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 9 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            updateTime = Utils.EpochTime();
            canvas.upperText.Display("You will receive it after you close the new level overlay.");
            canvas.DisableAll();
            progress++;
        }
        else if (progress == 10 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
        }
    }
}