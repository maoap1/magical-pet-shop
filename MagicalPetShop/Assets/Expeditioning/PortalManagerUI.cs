using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Initializes portals with necessaty data
public class PortalManagerUI : MonoBehaviour
{
    [SerializeField]
    ExpeditionsListUI expeditionsList;

    // Start is called before the first frame update
    void Start()
    {
        List<PortalUI> portals = new List<PortalUI>(gameObject.GetComponentsInChildren<PortalUI>());
        foreach (PortalUI portal in portals) {
            portal.SetExpeditionList(this.expeditionsList);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
