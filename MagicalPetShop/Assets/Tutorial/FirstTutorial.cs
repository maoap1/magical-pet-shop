using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "First Tutorial", menuName = "Tutorials/First Tutorial")]
public class FirstTutorial : Tutorial
{
    private long updateTime;
    public override bool finished()
    {
        if (progress==16)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
        }
        return progress==16;
    }

    public override void startWithProgress(int progress)
    {
        SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
        switcher.on = false;
        Crafting.randomImproveQuality = false;
        Shop.customersComing = false;
        if (progress < 5)
        {
            this.progress = 0;
        }
        else if (progress < 8)
        {
            this.progress = 3;
        }
        else if (progress == 8)
        {
            this.progress = 100;
        }
        else if (progress == 9)
        {
            this.progress = 110;
        }
        else if (progress < 14)
        {
            this.progress = 10;
        }
        else if (progress < 16)
        {
            this.progress = 13;
        }
    }

    public override bool tryStart()
    {
        Crafting.randomImproveQuality = false;
        progress = 0;
        return true;
    }

    public override void update()
    {
        if (progress==0)
        {
            PlayerState.THIS.Save();
            Shop.customersComing = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            
            canvas.DisableAll(true, true);

            GameObject customer = GameObject.Find("Canvas/SpawnPoint/TutorialCustomer");
            GameObject customerBlur = GameObject.Find("Canvas Blur/SpawnPoint/TutorialCustomer");
            foreach (Transform child in customer.transform)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Transform child in customerBlur.transform)
            {
                child.gameObject.SetActive(true);
            }

            updateTime = Utils.EpochTime();
            progress++;
        }
        if (progress==1 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Looks like there is a customer wanting to order a fish. Tap on him!");
            GameObject customer = GameObject.Find("Canvas/SpawnPoint/TutorialCustomer");
            GameObject customerBlur = GameObject.Find("Canvas Blur/SpawnPoint/TutorialCustomer");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 415;
            tp.top = 1070;
            tp.width = 250;
            tp.height = 550;
            canvas.SetMask(Vector3.zero, new Vector2(1080, 1920));
            canvas.DisableAllExcept(tp);
            foreach (Transform child in customer.transform)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Transform child in customerBlur.transform)
            {
                child.gameObject.SetActive(true);
            }
            progress++;
        }
        else if (progress==2 && GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Oh snap! It looks like we don't have fish in stock. Tell the customer to wait!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 340;
            tp.top = 1385;
            tp.width = 400;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==3 && !GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 20;
            tp.top = 1670;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            canvas.upperText.Display("Animals are crafted in the lab. Swipe left or use the navbar to go there.");
            canvas.leftArrow.SetActive(true);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
        }
        else if (progress==4 && SceneManager.GetActiveScene().name == "Lab")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.leftArrow.SetActive(false);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            canvas.DisableAll(true, true);
            updateTime = Utils.EpochTime();
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress==5 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.upperText.Display("Tap on the cauldron to open the crafting menu!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 90;
            tp.top = 900;
            tp.width = 900;
            tp.height = 700;
            canvas.SetMask(Vector3.zero, new Vector2(1080, 1920));
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==6 && GameLogic.THIS.inCrafting)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Click on the panel with the fish to start crafting it!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 60;
            tp.top = 680;
            tp.width = 320;
            tp.height = 500;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==7 && PlayerState.THIS.crafting.Count > 0)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("The fish is being crafted! Wait until it is finished!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 860;
            tp.top = 1495;
            tp.width = 200;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress==8 && PlayerState.THIS.crafting.Count > 0 && PlayerState.THIS.crafting[0].fillRate >= 1)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Crafting has finished! Collect the fish by clicking on it!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 860;
            tp.top = 1495;
            tp.width = 200;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress==9 && PlayerState.THIS.crafting.Count == 0)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Return to the shop to sell the fish!");
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 230;
            tp.top = 1670;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            canvas.rightArrow.SetActive(true);
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress==10 && SceneManager.GetActiveScene().name == "Shop")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialShop").GetComponent<TutorialCanvas>();
            canvas.rightArrow.SetActive(false);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            canvas.DisableAll(true, true);
            updateTime = Utils.EpochTime();
            GameObject customer = GameObject.Find("Canvas/SpawnPoint/TutorialCustomer");
            GameObject customerBlur = GameObject.Find("Canvas Blur/SpawnPoint/TutorialCustomer");
            foreach (Transform child in customer.transform)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Transform child in customerBlur.transform)
            {
                child.gameObject.SetActive(true);
            }
            progress++;
        }
        else if (progress==11 && Utils.EpochTime() - updateTime > 1000)
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialShop").GetComponent<TutorialCanvas>();
            canvas.upperText.Display("Tap on the customer to sell the fish to him!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 415;
            tp.top = 1070;
            tp.width = 250;
            tp.height = 550;
            canvas.SetMask(Vector3.zero, new Vector2(1080, 1920));
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==12 && GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Click on the sell button to sell the fish to the customer!");
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 555;
            tp.top = 1185;
            tp.width = 400;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==13 && !GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            GameObject customer = GameObject.Find("Canvas/SpawnPoint/TutorialCustomer");
            GameObject customerBlur = GameObject.Find("Canvas Blur/SpawnPoint/TutorialCustomer");
            foreach (Transform child in customer.transform)
            {
                child.gameObject.SetActive(false);
            }
            foreach (Transform child in customerBlur.transform)
            {
                child.gameObject.SetActive(false);
            }
            canvas.DisableAll();
            canvas.upperText.Display("The fish was sold congratulations! You earned 50 coins!");
            updateTime = Utils.EpochTime();
            progress++;
            PlayerState.THIS.Save();
        }
        else if (progress==14 && Utils.EpochTime()-updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAll();
            canvas.upperText.Display("As a reward you receive 100 more coins!");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress==15 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.lowerText.Close();
            Inventory.AddToInventory(100);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            progress++;
            PlayerState.THIS.Save();
        }











        else if (progress == 100)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 20;
            tp.top = 1670;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            canvas.upperText.Display("Move to the lab!");
            canvas.leftArrow.SetActive(true);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
        }
        else if (progress == 101 && SceneManager.GetActiveScene().name == "Lab")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.DisableAll(true, true);
            canvas.leftArrow.SetActive(false);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            updateTime = Utils.EpochTime();
            progress=7;
        }

        else if (progress == 110)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            TutorialPanel tp = new GameObject().AddComponent<TutorialPanel>();
            tp.left = 20;
            tp.top = 1670;
            tp.width = 200;
            tp.height = 250;
            canvas.DisableAllExcept(tp);
            canvas.upperText.Display("Move to the lab!");
            canvas.leftArrow.SetActive(true);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
        }
        else if (progress == 111 && SceneManager.GetActiveScene().name == "Lab")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.leftArrow.SetActive(false);
            canvas.DisableAll(true, true);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            updateTime = Utils.EpochTime();
            progress = 8;
        }
    }
}
