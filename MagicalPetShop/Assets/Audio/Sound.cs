using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "PetShop/Sound")]
public class Sound : Audio {
    public SoundType type;

    [HideInInspector]
    public AudioSource source;
}

public enum SoundType {
    Click,
    TabSwitch,
    Cash,
    Splash,
    CustomerAppear,
    Door,
    Steps,
    Cauldron,
    ExpeditionSuccessful,
    ExpeditionFailed,
    CraftingSuccessful,
    RecipeUpgrade
}