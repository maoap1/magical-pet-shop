using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadersButton : MonoBehaviour {

    private PackLeadersUI _leadersUI;

    public void SetPackLeaders(PackLeadersUI leaders) {
        this._leadersUI = leaders;
    }

    public void ShowPackLeaders() {
        this._leadersUI.Open();
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
