using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "First Tutorial", menuName = "Tutorials/First Tutorial")]
public class FirstTutorial : Tutorial
{
    private int progress;
    private long updateTime;
    public override bool finished()
    {
        if (progress==13)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Crafting.randomImproveQuality = true;
            Shop.customersComing = true;
        }
        return progress==13;
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
            Shop.customersComing = false;
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Looks like there is a customer wanting to order a fish. Tap on him!");
            GameObject customer = GameObject.Find("Canvas/SpawnPoint/TutorialCustomer");
            GameObject customerBlur = GameObject.Find("Canvas Blur/SpawnPoint/TutorialCustomer");
            canvas.DisableAllExcept(customer.GetComponent<TutorialPanel>());
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
        else if (progress==1 && GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Oh snap! It looks like we don't have fish in stock. Tell the customer to wait!");
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/OrderOverlay/Wait").GetComponent<TutorialPanel>());
            progress++;
        }
        else if (progress==2 && !GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Navbar/Layout/LabButton").GetComponent<TutorialPanel>());
            canvas.upperText.Display("Animals are crafted in the lab. Swipe left or use the navbar to go there.");
            progress++;
        }
        else if (progress==3 && SceneManager.GetActiveScene().name == "LabTutorial")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.upperText.Display("Tap on the cauldron to open the crafting menu!");
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Cauldron").GetComponent<TutorialPanel>());
            progress++;
        }
        else if (progress==4 && GameLogic.THIS.inCrafting)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.lowerText.Display("Click on the panel with the fish to start crafting it!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 60;
            tp.top = 680;
            tp.width = 320;
            tp.height = 420;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==5 && PlayerState.THIS.crafting.Count > 0)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.lowerText.Close();
            canvas.upperText.Display("The fish is being crafted! Wait until it is finished!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 860;
            tp.top = 1495;
            tp.width = 200;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==6 && PlayerState.THIS.crafting.Count > 0 && PlayerState.THIS.crafting[0].fillRate >= 1)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Crafting has finished! Collect the fish by clicking on it!");
            TutorialPanel tp = new TutorialPanel();
            tp.left = 860;
            tp.top = 1495;
            tp.width = 200;
            tp.height = 200;
            canvas.DisableAllExcept(tp);
            progress++;
        }
        else if (progress==7 && PlayerState.THIS.crafting.Count == 0)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Return to the shop to sell the fish!");
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Navbar/Layout/ShopButton").GetComponent<TutorialPanel>());
            progress++;
        }
        else if (progress==8 && SceneManager.GetActiveScene().name == "ShopTutorial")
        {
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
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialShop").GetComponent<TutorialCanvas>();
            canvas.upperText.Display("Tap on the customer to sell the fish to him!");
            canvas.DisableAllExcept(customer.GetComponent<TutorialPanel>());
            progress++;
        }
        else if (progress==9 && GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Click on the sell button to sell the fish to the customer!");
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/OrderOverlay/Sell").GetComponent<TutorialPanel>());
            progress++;
        }
        else if (progress==10 && !GameLogic.THIS.inSellingOverlay)
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
        }
        else if (progress==11 && Utils.EpochTime()-updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAll();
            canvas.upperText.Display("As a reward you receive 100 more coins!");
            updateTime = Utils.EpochTime();
            progress++;
        }
        else if (progress==12 && Utils.EpochTime() - updateTime > 2000)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            canvas.lowerText.Close();
            Inventory.AddToInventory(100);
            FindObjectOfType<AudioManager>().Play(SoundType.Cash);
            progress++;
        }
    }
}
