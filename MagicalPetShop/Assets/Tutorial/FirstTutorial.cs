using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "First Tutorial", menuName = "Tutorials/First Tutorial")]
public class FirstTutorial : Tutorial
{
    private int progress;
    public override bool finished()
    {
        if (progress==4)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.EnableAll();
            Shop.customersComing = true;
        }
        return progress==4;
    }

    public override bool tryStart()
    {
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
        else if (progress==1 & GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Display("Oh snap looks like we don't have fish in stock. Tell the customer to wait!");
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/OrderOverlay/Wait").GetComponent<TutorialPanel>());
            progress++;
        }
        else if (progress==2 & !GameLogic.THIS.inSellingOverlay)
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.DisableAllExcept(GameObject.Find("Canvas/SpawnPoint/Navbar/Layout/LabButton").GetComponent<TutorialPanel>());
            canvas.upperText.Display("Animals are crafted in the lab. Swipe right or use the navbar to go there.");
            progress++;
        }
        else if (progress==3 & SceneManager.GetActiveScene().name == "Lab")
        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            canvas.upperText.Close();
            Debug.Log("in the lab");
            progress++;
        }
    }
}
