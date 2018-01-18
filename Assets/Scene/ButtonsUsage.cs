using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsUsage : MonoBehaviour {
    
    public MyButton del;
    public MyButton playButton;

    void Start()
    {
        playButton.signalOnClick.AddListener(this.onPlay);
        del.signalOnClick.AddListener(this.delete);
    }
    void onPlay()
    {
        SceneManager.LoadScene("ChooseLevel");
    }
    void delete()
    {
        PlayerPrefs.DeleteAll();
    }
}
