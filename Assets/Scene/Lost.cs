using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lost : MonoBehaviour {
    public MyButton close;
    public MyButton restart;
    public MyButton menu;


    public AudioClip loseSound = null;
    AudioSource sound = null;

    void Start()
    {
        LevelController.current.createStats(false);
       
        close.signalOnClick.AddListener(this.onClose);
        restart.signalOnClick.AddListener(this.onRestart);
        menu.signalOnClick.AddListener(this.onPlay);
        sound = gameObject.AddComponent<AudioSource>();
        sound.clip = loseSound;
        LevelController.current.stopMusic();
        sound.Play();
        if (SoundManager.Instance.isMusicOn()) LevelController.current.playMusic();
    }

    void onClose()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
    void onPlay()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    void onRestart()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Level" + LevelController.current.level.ToString());
    }
}
