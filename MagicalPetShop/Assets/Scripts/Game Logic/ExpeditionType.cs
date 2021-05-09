using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ExpeditionType", menuName = "PetShop/Expedition Type")]
public class ExpeditionType : ScriptableObject
{
    public string name;
    public Sprite artwork;
    public int level;
    public int duration;
    public Artifact reward;
    public List<ExpeditionMode> difficultyModes;

    public string GetFormattedDuration() { 
        if (this.duration > 60) {
            int minutes = this.duration / 60;
            int seconds = this.duration % 60;
            if (minutes > 60) {
                return (minutes / 60) + " h " + (minutes % 60) + " m " + seconds + " s";
            } else {
                return minutes + " m " + seconds + " s";
            }
        } else {
            return this.duration + " s";
        }
    }
}


