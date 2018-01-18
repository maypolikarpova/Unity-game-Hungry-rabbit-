using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Win : MonoBehaviour {

    public MyButton close;
    public MyButton restart;
    public MyButton next;

    public UILabel fruits;
    public UILabel coins;

    public UI2DSprite gem1;
    public Sprite gem11;
    public UI2DSprite gem2;
    public Sprite gem22;
    public UI2DSprite gem3;
    public Sprite gem33;

    public AudioClip winSound = null;
    AudioSource sound = null;

    void Start()
    {
        LevelController.current.createStats(true);
     
        close.signalOnClick.AddListener(this.onClose);
        restart.signalOnClick.AddListener(this.onRestart);
        next.signalOnClick.AddListener(this.onPlay);
        coins.text = LevelController.current.coins_quantity.ToString();
        fruits.text = LevelController.current.fruits_quantity.ToString() + "/" + LevelController.current.fruits_total.ToString();
        if (LevelController.current.blue)
        {
            gem1.sprite2D = gem11;
        }
        if (LevelController.current.green)
        {
            gem2.sprite2D = gem22;
        }
        if (LevelController.current.red)
        {
            gem3.sprite2D = gem33;
        }

        sound = gameObject.AddComponent<AudioSource>();
        sound.clip = winSound;
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
        SceneManager.LoadScene("ChooseLevel");
    }
    void onRestart()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene("Level"+LevelController.current.level.ToString());
    }
}
