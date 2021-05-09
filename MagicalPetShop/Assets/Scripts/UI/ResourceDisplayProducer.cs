using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceDisplayProducer : MonoBehaviour
{
    public Image imageComponent;
    public Sprite essenceImage;
    public TextMeshProUGUI text;
    public Button buttonProducer;
    public Essence essence;
    private EssenceAmount essenceAmount = null;
    private bool unlockedImage = false;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent.sprite = essenceImage;
        imageComponent.color = Color.black;
        buttonProducer.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (essenceAmount == null && PlayerState.THIS.resources != null && PlayerState.THIS.resources.Count > 0)
        {
            foreach (EssenceAmount r in PlayerState.THIS.resources)
            {
                if (r.essence.essenceName == essence.essenceName)
                {
                    essenceAmount = r;
                }
            }
        }
        else if (essenceAmount != null)
        {
            if (essenceAmount.unlocked & !unlockedImage)
            {
                buttonProducer.interactable = true;
                //imageComponent.sprite = essenceImage;
                imageComponent.color = Color.white;
                unlockedImage = true;
            }
            text.text = essenceAmount.amount.ToString();
            if (essenceAmount.full)
            {
                text.color = new Color(0, 168, 0);
            }
            else
            {
                text.color = Color.white;
            }
        }
    }
}
