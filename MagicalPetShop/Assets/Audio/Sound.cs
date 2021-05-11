using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An action sound with a specific type
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
    Success,
    Fail,
    Crafting,
    RecipeUpgrade
}