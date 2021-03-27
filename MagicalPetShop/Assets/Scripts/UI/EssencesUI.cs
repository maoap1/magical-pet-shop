using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencesUI : MonoBehaviour
{

    [SerializeField]
    List<EssenceUI> essences;

    public void Refresh() {
        foreach (EssenceUI essence in this.essences) {
            essence.Refresh();
        }
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
