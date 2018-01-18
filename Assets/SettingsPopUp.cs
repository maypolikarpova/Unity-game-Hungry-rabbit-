using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour {

    public MyButton close;
    public MyButton music;
    public MyButton sound;
   
    public Sprite onm;
    public Sprite offm;
    public Sprite ons;
    public Sprite offs;
    public UI2DSprite mus;
    public UI2DSprite soun;

    void Start()
    {
        close.signalOnClick.AddListener(this.onPlay);
        music.signalOnClick.AddListener(this.onMusic);
        sound.signalOnClick.AddListener(this.onSound);
    }

    void onPlay()
    {
        Destroy(this.gameObject);
    }

    void onMusic()
    {
        if (SoundManager.Instance.isMusicOn())
        {
            SoundManager.Instance.setMusicOn(false);
            mus.sprite2D = offm;
        }
        else
        {
            SoundManager.Instance.setMusicOn(true);
            mus.sprite2D = onm;
        }
    }

    void onSound()
    {
        if (SoundManager.Instance.isSoundOn())
        {
            SoundManager.Instance.setSoundOn(false);
            soun.sprite2D = offs;
        }
        else
        {
            SoundManager.Instance.setSoundOn(true);
            soun.sprite2D = ons;
        }
    }
}
