using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeProducer : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI productionRate;
    public TextMeshProUGUI essenceInStock;
    public TextMeshProUGUI cost;
    public GameObject upgradePanel;
    public Button upgrade;
    [HideInInspector]
    public EssenceProducer producer;

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;

    private int upgradeCost;

    public void Update()
    {
        essenceInStock.text = producer.essenceAmount.amount.ToString();
        if (PlayerState.THIS.money >= upgradeCost)
        {
            cost.color = Color.black;
            upgrade.interactable = true;
        }
        else
        {
            cost.color = Color.red;
            upgrade.interactable = false;
        }
    }
    public void Open(Essence e)
    {
        producer = PlayerState.THIS.producers.Find(x => x.essenceAmount.essence.essenceName == e.essenceName);
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(false);
        }
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        productionRate.text = producer.productionRate.ToString();
        name.text = producer.essenceAmount.essence.essenceName;
        image.sprite = producer.essenceAmount.essence.icon;
        upgradeCost = producer.model.GetCost(producer.level + 1);
        if (upgradeCost == -1)
        {
            upgradePanel.SetActive(false);
        }
        else
        {
            upgradePanel.SetActive(true);
            cost.text = upgradeCost.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(upgradePanel.GetComponent<RectTransform>());
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        foreach (GameObject g in objectsToAppear)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in objectsToHide)
        {
            g.SetActive(true);
        }
    }

    public void Upgrade()
    {
        if (PlayerState.THIS.money >= upgradeCost)
        {
            producer.UpgradeProducer();
        }
        UpdateInfo();
    }
}
