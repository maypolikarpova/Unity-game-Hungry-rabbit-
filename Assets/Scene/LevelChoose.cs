using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoose : MonoBehaviour {
    public static bool firstEver = true;

    public UI2DSprite doorCrystals;
    public Sprite doorCrystalsCollected;
    public  UI2DSprite doorFruits;
    public Sprite doorFruitsCollected;

    public UI2DSprite doorPassed;
    public Sprite passed;
    
    LevelStats cur = new LevelStats();
    
    public int level;
   
    public GameObject win;
    // Use this for initialization
    void Start () {
       
	}
    void addInfo()
    {
            string str = PlayerPrefs.GetString("stats" + level.ToString());
            cur = JsonUtility.FromJson<LevelStats>(str);

        if (cur != null)
        {
            if (cur.hasAllCrystals) doorCrystals.sprite2D = doorCrystalsCollected;
            if (cur.hasAllFruits) doorFruits.sprite2D = doorFruitsCollected;
            if (cur.levelPassed)
            {
                doorPassed.sprite2D = passed;
            }
        }
    }

    public bool isOpen()
    {
        if (!firstEver && JsonUtility.FromJson<LevelStats>(PlayerPrefs.GetString("stats" + (level - 1).ToString())).levelPassed)
        {
            return true;
        }
        else return false;
    }
	// Update is called once per frame
	void Update () {

        if (!firstEver && level > 1 && isOpen()) doorPassed.sprite2D = null;
        if (level != 0) addInfo();
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (level==0)
        {
            GameObject parent = UICamera.first.transform.parent.gameObject;
            GameObject obj = NGUITools.AddChild(parent, win);
            Win popup = obj.GetComponent<Win>();
            popup.transform.position = parent.transform.position;
            firstEver = false;

        }

        else if (level == 1)
        {
            SceneManager.LoadScene("Level1");
           
        }

        else if (level == 2&&isOpen())
        {
            SceneManager.LoadScene("Level2");

        }
    }
}
