using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public AudioSource audioSource;

    GameAudio gameAudio;

    void Awake() {
        BackgroundMusic[] objs = GameObject.FindObjectsOfType<BackgroundMusic>();
        if (objs.Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameAudio = GameAudio.THIS;
        if (this.gameAudio == null) return;
        
        PlayNextMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.audioSource.isPlaying) {
            PlayNextMusic();
        }
    }

    private void PlayNextMusic() {
        if (this.gameAudio == null)
            this.gameAudio = GameAudio.THIS;
        if (this.gameAudio == null) return;

        int index = Random.Range(0, this.gameAudio.backgroundMusic.Count);
        Audio music = this.gameAudio.backgroundMusic[index];
        this.audioSource.PlayOneShot(music.clip, music.volume);
    }
}
