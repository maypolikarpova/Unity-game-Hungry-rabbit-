using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    public GameObject settingsPrefab;
    public MyButton show;
   
    void showSettings()
    {
        GameObject parent = UICamera.first.transform.parent.gameObject;
        
        GameObject obj = NGUITools.AddChild(parent, settingsPrefab);
        
        SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();
        popup.transform.position = parent.transform.position;
    }

  

    void Start()
    {
        show.signalOnClick.AddListener(this.onPlay);
      

    }

    void onPlay()
    {
        showSettings();
    }

}
