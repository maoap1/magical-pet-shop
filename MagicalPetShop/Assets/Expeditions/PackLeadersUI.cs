using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackLeadersUI : MonoBehaviour {

    public List<GameObject> objectsToHide;

    public void Open() {
        Refresh();
        this.gameObject.SetActive(true);
        foreach (GameObject g in objectsToHide) {
            g.SetActive(false);
        }
    }

    public void Close() {
        foreach (GameObject g in objectsToHide) {
            g.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }

    public void Refresh() {
        // clear everything
        Clear();
        // display current info
        DisplayItems();
    }

    private void DisplayItems() {
    }

    private void Clear() {
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
