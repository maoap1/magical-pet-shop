using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System;

public class Analytics : MonoBehaviour
{
    [Tooltip("Variant of game for experiment, change before build (0 is reserved for debugging).")]
    public int buildVariant = 0;

    static bool initialized;
    static List<AnalyticsEvent> pendingEvents = new List<AnalyticsEvent>();

    double elapsedTime;
    double interval = 30;

    bool isFirst = false;


    public static void LogEvent(string eventName) {
        if (Analytics.initialized) {
            FirebaseAnalytics.LogEvent(eventName, new Parameter("build_variant", GameObject.FindObjectOfType<Analytics>().buildVariant));
        } else {
            Analytics.pendingEvents.Add(new AnalyticsEvent(eventName));
        }
    }

    public static void LogEvent(string eventName, params Parameter[] parameters) {
        if (Analytics.initialized) {
            Array.Resize(ref parameters, parameters.Length + 1);
            parameters[parameters.Length - 1] = new Parameter("build_variant", GameObject.FindObjectOfType<Analytics>().buildVariant);
            FirebaseAnalytics.LogEvent(eventName, parameters);
        } else {
            Analytics.pendingEvents.Add(new AnalyticsEvent(eventName, parameters));
        } 
    }

    private void Awake() {
        Analytics[] objs = GameObject.FindObjectsOfType<Analytics>();
        if (objs.Length > 1) {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase should be available");

                Analytics.LogEvent("game_started");
                
                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Analytics.initialized = true;
                this.isFirst = true;
            } else {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        this.elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Analytics.initialized && Analytics.pendingEvents.Count > 0) {
            foreach (var e in Analytics.pendingEvents) {
                if (e.parameters == null) {
                    Analytics.LogEvent(e.eventName);
                } else {
                    Analytics.LogEvent(e.eventName, e.parameters);
                }
            }
            Analytics.pendingEvents.Clear();
        }

        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime > this.interval) {
            this.elapsedTime = 0;
            Analytics.LogEvent("resources", new Parameter("money", PlayerState.THIS.money), new Parameter("inventory_animals", Inventory.GetNumberOfAnimals()), new Parameter("inventory_artifacts", Inventory.GetNumberOfArtifacts()));
        }
    }

    private void OnDestroy() {
        if (this.isFirst) {
            Analytics.LogEvent("game_quited");
        }
    }
}

public class AnalyticsEvent {
    public string eventName;
    public Parameter[] parameters;

    public AnalyticsEvent(string eventName) {
        this.eventName = eventName;
    }

    public AnalyticsEvent(string eventName, Parameter[] parameters) {
        this.eventName = eventName;
        this.parameters = parameters;
    }
}
