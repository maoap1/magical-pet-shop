using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepeatedSound", menuName = "PetShop/Repeated Sound")]
public class RepeatedSound : Audio {
    public int minInterval;
    public int maxInterval;

    public List<AudioClip> variations;
    public List<float> volumes;

    [HideInInspector]
    public bool shouldPlay;

    [HideInInspector]
    public AudioSource source;
}
