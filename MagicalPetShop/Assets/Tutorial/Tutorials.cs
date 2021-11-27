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
    public int playedIndex = -1;
    private long lastUpdateTime;

    public bool finished = false;
    public bool settingsDisabled = false;

    private bool current_finished = false;

    public void Update()
    {
        if (!finished)
        {
            lastUpdateTime = Utils.EpochTime();
            if (playedIndex >= 0)
            {
                tutorials[playedIndex].update();
                if (tutorials[playedIndex].finished())
                {
                    Analytics.LogEvent("tutorial_ended", new Parameter("tutorial_name", tutorials[playedIndex].tutorialName));
                    current_finished = true;
                    playedIndex = -1;
                }
            }
            else
            {
                if (tutorials.Count == 0 || (currentIndex + 1 == tutorials.Count && tutorials[currentIndex].finished()))
                {
                    finished = true;
                }
                else if (currentIndex == -1 || (currentIndex >= 0 && tutorials[currentIndex].finished()))
                {
                    while (currentIndex + 1 < tutorials.Count && tutorials[currentIndex+1].finished())
                    {
                        currentIndex++;
                    }
                    for (int i = currentIndex + 1; i<tutorials.Count; i++)
                    {
                        if (!tutorials[i].finished() && tutorials[i].tryStart())
                        {
                            playedIndex = i;
                            Analytics.LogEvent("tutorial_started", new Parameter("tutorial_name", tutorials[playedIndex].tutorialName));
                            currentIndex++;
                            current_finished = false;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void init()
    {
        lastUpdateTime = Utils.EpochTime() - 500;
        finished = false;
        currentIndex = -1;
        playedIndex = -1;
}
}
