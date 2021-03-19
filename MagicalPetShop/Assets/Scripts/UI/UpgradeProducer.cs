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
    public GameObject costPanel;
    public GameObject costPrefab;
    public Button upgrade;
    [HideInInspector]
    public EssenceProducer producer;

    public List<GameObject> objectsToAppear;
    public List<GameObject> objectsToHide;

    private Cost upgradeCost;

    public void Update()
    {
        essenceInStock.text = producer.essenceAmount.ToString();
    }
    public void Open(Essence e)
    {
        producer = PlayerState.THIS.producers.Find(x => x.essenceName == e.essenceName);
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
        name.text = producer.essenceName;
        image.sprite = producer.essenceIcon;
        upgradeCost = producer.model.GetCost(producer.level + 1);
        foreach (Transform child in costPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        ResourceCost cm = Instantiate(costPrefab, costPanel.transform).GetComponent<ResourceCost>();
        cm.SetCost(image.sprite, upgradeCost.money);
        foreach (EssenceCount es in upgradeCost.resources)
        {
            ResourceCost ce = Instantiate(costPrefab, costPanel.transform).GetComponent<ResourceCost>();
            ce.SetCost(es.essence.icon, es.count);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(costPanel.GetComponent<RectTransform>());
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
        if (PlayerState.THIS.money > upgradeCost.money)
        {
            producer.UpgradeProducer();
        }
        UpdateInfo();
    }
}
