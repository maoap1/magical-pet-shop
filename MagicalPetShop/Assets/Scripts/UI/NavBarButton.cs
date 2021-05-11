using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NavBarButton : MonoBehaviour {

    public Sprite icon;
    public string text;

    [SerializeField]
    Image iconImage;
    [SerializeField]
    TextMeshProUGUI textTMP;



    // Start is called before the first frame update
    void Start()
    {
        this.iconImage.sprite = this.icon;
        this.textTMP.text = this.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
