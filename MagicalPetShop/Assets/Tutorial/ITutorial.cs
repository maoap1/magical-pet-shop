using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : ScriptableObject
{
    public abstract bool tryStart();
    public abstract void update();
    public abstract bool finished();
    public abstract void startWithProgress(int progress);
    public int progress;
}
