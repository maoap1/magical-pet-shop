using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AppearHideComponent))]
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

    private int upgradeCost;

    private Color defaultTextColor = Color.clear;

    public void Update() {
        if (defaultTextColor == Color.clear) InitializeColor();
        essenceInStock.text = producer.essenceAmount.amount.ToString() + "/" + producer.storageLimit.ToString();
        if (PlayerState.THIS.money >= upgradeCost)
        {
            cost.color = this.defaultTextColor;
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
        GetComponent<AppearHideComponent>().Do();
        Show(e);
    }

    public void Show(Essence e) {
        producer = PlayerState.THIS.producers.Find(x => x.essenceAmount.essence.essenceName == e.essenceName);
        GameLogic.THIS.essenceProducerOpened = producer;
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
        GameLogic.THIS.essenceProducerOpened = null;
        GetComponent<AppearHideComponent>().Revert();
    }

    public void Upgrade()
    {
        if (PlayerState.THIS.money >= upgradeCost)
        {
            producer.UpgradeProducer();
        }
        UpdateInfo();
    }

    private void InitializeColor() {
        TMPColor colorComponent = cost.gameObject.GetComponent<TMPColor>();
        if (colorComponent != null)
            this.defaultTextColor = UIPalette.THIS.GetColor(colorComponent.color);
        else {
            this.defaultTextColor = Color.black;
        }
    }
}
