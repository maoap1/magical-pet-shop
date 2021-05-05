using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{

    GameAudio gameAudio;

    SceneAudio currentSceneAudio;

    void Awake() {
        AudioManager[] objs = GameObject.FindObjectsOfType<AudioManager>();
        if (objs.Length > 1) {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        this.gameAudio = GameAudio.THIS;
        if (this.gameAudio == null) {
            Debug.Log("GameAudio.THIS returned null!");
            return;
        }
        foreach (Sound sound in this.gameAudio.sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
        }
        foreach (SceneAudio sceneAudio in this.gameAudio.scenesAudio) {
            if (sceneAudio.sceneName == SceneManager.GetActiveScene().name)
                this.currentSceneAudio = sceneAudio;
            foreach (RepeatedSound sound in sceneAudio.backgroundSounds) {
                BackgroundSound bgs = gameObject.AddComponent<BackgroundSound>();
                bgs.sound = sound;
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.shouldPlay = false;
            }
            foreach (AmbientSound sound in sceneAudio.ambientSounds) { 
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = 0;
                sound.source.loop = true;
            }
        }
        StartPlayingScene(this.currentSceneAudio);
    }

    public void Start() {
    }

    public void Update() {
        if (currentSceneAudio.sceneName != SceneManager.GetActiveScene().name) {
            StopPlayingScene(this.currentSceneAudio);
            foreach (SceneAudio sceneAudio in this.gameAudio.scenesAudio) {
                if (sceneAudio.sceneName == SceneManager.GetActiveScene().name)
                    this.currentSceneAudio = sceneAudio;
            }
            StartPlayingScene(this.currentSceneAudio);
        }
    }

    public void Play(SoundType type) {
        if (this.gameAudio == null) this.gameAudio = GameAudio.THIS;
        if (this.gameAudio == null) {
            Debug.Log("GameAudio.THIS returned null!");
            return;
        }
        Sound sound = this.gameAudio.GetSound(type);
        if (sound != null) {
            sound.source.PlayOneShot(sound.clip, sound.volume);
        }
    }

    private void StartPlayingScene(SceneAudio sceneAudio) {
        foreach (RepeatedSound sound in sceneAudio.backgroundSounds) {
            sound.shouldPlay = true;
        }
        foreach (AmbientSound sound in sceneAudio.ambientSounds) {
            if (!sound.source.isPlaying) sound.source.Play();
            sound.source.DOFade(sound.volume, sound.fadeInLength);
        }
    }

    private void StopPlayingScene(SceneAudio sceneAudio) {
        foreach (RepeatedSound sound in sceneAudio.backgroundSounds) {
            sound.shouldPlay = false;
            if (sound.source.isPlaying) sound.source.DOFade(0, this.gameAudio.sceneSwitchFadeOut);
        }
        foreach (AmbientSound sound in sceneAudio.ambientSounds) {
            sound.source.DOFade(0, this.gameAudio.sceneSwitchFadeOut);
        }
    }

}
