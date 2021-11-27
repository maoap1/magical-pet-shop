        {
            TutorialCanvas canvas = Resources.FindObjectsOfTypeAll<TutorialCanvas>()[0];
            Rect tp = new Rect
            {
                x = 20,
                y = 1670,
                width = 200,
                height = 250
            };
            canvas.DisableAllExcept(tp);
            canvas.upperText.Display("Move to the lab!");
            canvas.leftArrow.SetActive(true);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = true;
            progress++;
        }
        else if (progress == 111 && SceneManager.GetActiveScene().name == "Lab")
        {
            TutorialCanvas canvas = GameObject.Find("CanvasTutorialLab").GetComponent<TutorialCanvas>();
            canvas.leftArrow.SetActive(false);
            canvas.DisableAll(true, true);
            SceneSwitcher switcher = Resources.FindObjectsOfTypeAll<SceneSwitcher>()[0];
            switcher.on = false;
            updateTime = Utils.EpochTime();
            progress = 9;
        }
    }
}