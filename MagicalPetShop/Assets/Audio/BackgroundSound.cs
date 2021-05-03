using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public RepeatedSound sound;
    public bool shouldPlay = false;

    private bool stopped = false;
    private long interval = 0;

    private long lastUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.sound.source.isPlaying && this.sound.shouldPlay) {
            if (!this.stopped) {
                this.stopped = true;
                this.interval = Random.Range(this.sound.minInterval, this.sound.maxInterval + 1) * 1000;
            } else {
                if (this.interval < 0) {
                    this.stopped = false;
                    AudioClip clip;
                    if (this.sound.clip != null) clip = this.sound.clip;
                    else clip = this.sound.variations[Random.Range(0, this.sound.variations.Count)];
                    this.sound.source.PlayOneShot(clip, this.sound.volume);
                } else {
                    this.interval -= Utils.EpochTime() - lastUpdate;
                }
            }
        }
        this.lastUpdate = Utils.EpochTime();
    }
}
