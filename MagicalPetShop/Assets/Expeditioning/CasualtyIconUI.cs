using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used in a summary of expedition results
public class CasualtyIconUI : MonoBehaviour
{
    [SerializeField]
    Image iconImage;

    public void Initialize(Animal animal) {
        this.iconImage.sprite = animal.artwork;
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
