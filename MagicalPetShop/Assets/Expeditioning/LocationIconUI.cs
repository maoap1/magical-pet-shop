using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Used in LocationAnimaSlotUI to show animal's traits
public class LocationIconUI : MonoBehaviour
{
    [SerializeField]
    Image iconImage;

    public void Initialize(LocationType location) {
        this.iconImage.sprite = location.artwork;
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
