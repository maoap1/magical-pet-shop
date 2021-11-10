using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class FirebaseInit : MonoBehaviour
{

    private void Awake() {
        FirebaseInit[] objs = GameObject.FindObjectsOfType<FirebaseInit>();
        if (objs.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase should be available");

                FirebaseAnalytics.LogEvent("game_started", new Parameter("build_variant", 0)); // TODO: Change number before build (0 is reserved for debugging)

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
}
