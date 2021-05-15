using UnityEngine;

public class SwipeSceneSwitcher : MonoBehaviour {
    public void LoadSceneOfName(string sceneName) {
        if (sceneName == "Yard" && PlayerState.THIS.level < 3) return;
        GameObject.FindObjectOfType<SceneSwitcher>().LoadScene(sceneName);
    }

}
