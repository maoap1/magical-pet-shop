using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoneyLabel : MonoBehaviour
{
    private Text moneyText;

    private Tween moneyTween;

    private int lastMoney;

    private int moneyTmp;

    // Start is called before the first frame update
    void Start()
    {
        this.moneyText = gameObject.GetComponent<Text>();
        this.moneyTween = null;
        this.lastMoney = PlayerState.THIS.money;
        this.moneyTmp = PlayerState.THIS.money;
        moneyText.text = PlayerState.THIS.money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerState.THIS.money != this.lastMoney) 
        {
            if (this.moneyTween != null && this.moneyTween.IsActive()) 
            {
                this.moneyTween.Kill();
                this.moneyTween = null;
            }
            this.lastMoney = PlayerState.THIS.money;
            this.moneyTween = DOTween.To(
                    () => this.moneyTmp,
                    m => { this.moneyTmp = m; this.moneyText.text = m.ToString(); Debug.Log(m); },
                    PlayerState.THIS.money,
                    1f)
                .SetEase(Ease.OutCubic);
        }
    }
}
