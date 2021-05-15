using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoneyLabel : MonoBehaviour
{
    private Text moneyText;
    private PlayerState playerState;

    private Tween moneyTween;

    private int lastMoney;

    private int moneyTmp;

    // Start is called before the first frame update
    void Start()
    {
        this.moneyText = gameObject.GetComponent<Text>();
        this.moneyTween = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.playerState == null) {
            this.playerState = PlayerState.THIS;
            if (this.playerState == null) return;
            else {
                this.lastMoney = this.playerState.money;
                this.moneyTmp = this.playerState.money;
            }
        }

        if (this.playerState.money != this.lastMoney) {
            if (this.moneyTween != null && this.moneyTween.IsActive()) {
                this.moneyTween.Kill();
            }
            this.moneyTween = null;
            this.lastMoney = this.playerState.money;
            this.moneyTween = DOTween.To(() => this.moneyTmp, m => { this.moneyTmp = m; this.moneyText.text = m.ToString(); }, this.playerState.money, 1f).SetEase(Ease.OutCubic);
        }
    }
}
