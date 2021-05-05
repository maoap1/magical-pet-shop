using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmbientSound", menuName = "PetShop/Ambient Sound")]
public class AmbientSound : Audio
{
    [Range(0f, 10f)]
    public float fadeInLength;

    [HideInInspector]
    public AudioSource source;
}
