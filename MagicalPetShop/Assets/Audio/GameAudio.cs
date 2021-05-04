using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAudio", menuName = "PetShop/Game Audio")]
public class GameAudio : ScriptableObject {

    public List<Sound> sounds;
    public List<Audio> backgroundMusic;

    public List<SceneAudio> scenesAudio;

    private static GameAudio _THIS;
    public static GameAudio THIS {
        get {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<GameAudio>();
            if (_THIS == null) {
                _THIS = Resources.Load<GameAudio>("GameAudio");
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    public Sound GetSound(AudioType type) {
        List<Sound> soundsFound = new List<Sound>();
        foreach (Sound sound in this.sounds) {
            if (type == sound.type)
                soundsFound.Add(sound);
        }
        if (soundsFound.Count == 0) return null;
        // randomly choose one sound of the given type
        return soundsFound[Random.Range(0, soundsFound.Count)];
    }
}

[System.Serializable]
public class SceneAudio {
    public string sceneName;
    public List<RepeatedSound> backgroundSounds;
}

[System.Serializable]
public class Audio {
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
}

[System.Serializable]
public class Sound : Audio {
    public AudioType type;

    [HideInInspector]
    public AudioSource source;
}

[System.Serializable]
public class RepeatedSound : Audio {
    public List<AudioClip> variations;
    public int minInterval;
    public int maxInterval;

    [HideInInspector]
    public bool shouldPlay;

    [HideInInspector]
    public AudioSource source;
}

public enum AudioType {
    Click,
    TabSwitch,
    Cash,
    Splash,
    CustomerAppear,
    Door,
    Steps,
    Cauldron,
    ExpeditionSuccessful,
    ExpeditionFailed,
    CraftingSuccessful,
    RecipeUpgrade
}
