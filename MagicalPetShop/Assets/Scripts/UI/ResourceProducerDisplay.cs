using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceProducerDisplay : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public Essence essence;
    private EssenceProducer producer = null;
    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = essence.icon;
    }

    // Update is called once per frame
    void Update()
    {
        if (producer==null && PlayerState.THIS.producers.Count>0)
        {
            foreach (EssenceProducer p in PlayerState.THIS.producers)
            {
                if (p.essenceName == essence.essenceName)
                {
                    producer = p;
                }
            }
        }
        text.text = producer.essenceAmount.ToString();
    }
}
