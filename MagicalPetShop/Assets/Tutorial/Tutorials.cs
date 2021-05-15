using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Logic", menuName = "PetShop/Game Logic")]
public class Tutorials : ScriptableObject
{
    private static Tutorials _THIS;
    public static Tutorials THIS
    {
        get
        {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<Tutorials>();
            if (_THIS == null)
            {
                _THIS = Resources.Load<Tutorials>("Tutorials");
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    public List<ITutorial> tutorials;
    public int currentIndex = -1;
    private long lastUpdateTime;

    public void Update()
    {
        if (Utils.EpochTime()-lastUpdateTime>500)
        {
            lastUpdateTime = Utils.EpochTime();
            if (currentIndex==-1 || (currentIndex>=0 && tutorials[currentIndex].finished()))
            {
                tutorials[currentIndex].tryStart();
            }
            else if (currentIndex >= 0)
            {
                tutorials[currentIndex].update();
            }
        }
    }
}
