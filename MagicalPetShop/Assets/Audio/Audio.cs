using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The most basic audio representation, without AudioSource
[CreateAssetMenu(fileName = "Audio", menuName = "PetShop/Audio")]
public class Audio : ScriptableObject {
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
}
