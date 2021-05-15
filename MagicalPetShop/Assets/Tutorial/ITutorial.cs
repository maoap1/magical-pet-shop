using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITutorial
{
    bool tryStart();
    void update();
    bool finished();
}
