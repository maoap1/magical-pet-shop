using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public RepeatedSound sound;
    public bool shouldPlay = false;

    private int lastSelected = -1;

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
            if (!this.stopped) { // the sound stopped just now, should be repeated after some interval
                this.stopped = true;
                this.interval = Random.Range(this.sound.minInterval, this.sound.maxInterval + 1) * 1000;
            } else {
                if (this.interval < 0) { // the interval is over, next sound should be played
                    this.stopped = false;
                    AudioClip clip;
                    // prefer variations, if there are any, randomly choose one
                    if (this.sound.variations.Count > 0) {
                        // include also a separate clip, if there is any (because of mistake)
                        int count = this.sound.variations.Count + (this.sound.clip != null ? 1 : 0);
                        int index = Random.Range(0, count);
                        while (index == this.lastSelected && count > 1) { // don't choose the same one as before
                            index = Random.Range(0, count);
                        }
                        this.lastSelected = index;
                        if (index == this.sound.variations.Count) clip = this.sound.clip;
                        else clip = this.sound.variations[index];
                        if (index < this.sound.volumes.Count) {
                            this.sound.source.PlayOneShot(clip, this.sound.volumes[index]);
                        } else {
                            this.sound.source.PlayOneShot(clip, this.sound.volume);
                        }
                    } else if (this.sound.clip != null) {
                        clip = this.sound.clip;
                        this.sound.source.PlayOneShot(clip, this.sound.volume);
                    }
                } else {
                    this.interval -= Utils.EpochTime() - lastUpdate;
                }
            }
        }
        this.lastUpdate = Utils.EpochTime();
    }
}
