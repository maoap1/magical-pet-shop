using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorial : ScriptableObject, ITutorial
{
    private int progress;
    public bool finished()
    {
        return false;
    }

    public bool tryStart()
    {
        progress = 0;
        return true;
    }

    public void update()
    {
        progress = 0;
    }
}
