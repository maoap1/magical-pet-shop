using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceUI : MonoBehaviour
{
    [SerializeField]
    private Essence essence;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text countText;
    [SerializeField]
    private Sprite unknownIcon;

    public void Refresh() {
        if (PlayerState.THIS.producers != null) {
            foreach (EssenceProducer producer in PlayerState.THIS.producers) {
                if (producer.essenceAmount.essence == this.essence) {
                    // set count
                    this.countText.text = producer.essenceAmount.amount.ToString();
                    if (producer.essenceAmount.full) this.countText.color = new Color(0, 168, 0);
                    else this.countText.color = Color.black;
                    // set icon
                    if (producer.level > 0) this.icon.sprite = this.essence.icon;
                    else this.icon.sprite = this.unknownIcon;
                    break;

                }
            }
        } else {
            this.countText.text = "0";
            this.icon.sprite = this.unknownIcon;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
