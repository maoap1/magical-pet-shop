using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalUI : MonoBehaviour
{
    [SerializeField]
    private int expeditionLevel;

    private ExpeditionsListUI expeditionsList;

    public void SetExpeditionList(ExpeditionsListUI expeditionsList) {
        this.expeditionsList = expeditionsList;
    }

    public void ShowExpeditions() {
        this.expeditionsList.Open(this.expeditionLevel);
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
