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