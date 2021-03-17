using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    [SerializeField]
    private GridLayoutGroup animalsGrid;

    public void Refresh() {
        // clear everything
        Clear();
        // add items from inventory to the grid layout

    }

    private void Clear() {
        int c = animalsGrid.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
            GameObject.Destroy(animalsGrid.transform.GetChild(i).gameObject);
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
