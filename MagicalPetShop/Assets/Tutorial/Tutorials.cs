using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorials", menuName = "PetShop/Tutorials")]
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

    public List<Tutorial> tutorials;
    public int currentIndex = -1;
    private long lastUpdateTime;

    private bool finished = false;
    public bool settingsDisabled = false;

    public void Update()
    {
        if (!finished)
        {
            lastUpdateTime = Utils.EpochTime();
            if (tutorials.Count == 0 || (currentIndex+1 == tutorials.Count && tutorials[currentIndex].finished()))
            {
                finished = true;
            }
            else if (currentIndex==-1 || (currentIndex>=0 && tutorials[currentIndex].finished()))
            {
                if (tutorials[currentIndex+1].tryStart())
                {
                    currentIndex++;
                }
            }
            else if (currentIndex >= 0)
            {
                tutorials[currentIndex].update();
            }
        }
    }

    public void init()
    {
        lastUpdateTime = Utils.EpochTime() - 500;
        finished = false;
        currentIndex = -1;
    }
}
