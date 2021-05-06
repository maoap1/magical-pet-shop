using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An ambient sound which is played in a loop throughout the whole scene
[CreateAssetMenu(fileName = "AmbientSound", menuName = "PetShop/Ambient Sound")]
public class AmbientSound : Audio
{
    [Range(0f, 10f)]
    public float fadeInLength;

    [HideInInspector]
    public AudioSource source;
}
