using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Image imageComponent = gameObject.GetComponent<Image>();
        imageComponent.sprite = GameGraphics.THIS.money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
