using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

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

    public bool finished = false;
    public bool settingsDisabled = false;

    private bool current_finished = false;

    public void Update()
    {
        if (!finished)
        {
            lastUpdateTime = Utils.EpochTime();
            if (tutorials.Count == 0 || (currentIndex+1 == tutorials.Count && tutorials[currentIndex].finished()))
            {
                if (tutorials.Count > 0 && !finished) {
                    Analytics.LogEvent("tutorial_ended", new Parameter("tutorial_name", tutorials[currentIndex].tutorialName));
                }
                finished = true;
                current_finished = true;
            }
            else if (currentIndex==-1 || (currentIndex>=0 && tutorials[currentIndex].finished())) {
                if (currentIndex >= 0 && !current_finished) {
                    Analytics.LogEvent("tutorial_ended", new Parameter("tutorial_name", tutorials[currentIndex].tutorialName));
                    current_finished = true;
                }
                if (tutorials[currentIndex+1].tryStart()) {
                    Analytics.LogEvent("tutorial_started", new Parameter("tutorial_name", tutorials[currentIndex + 1].tutorialName));
                    currentIndex++;
                    current_finished = false;
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
