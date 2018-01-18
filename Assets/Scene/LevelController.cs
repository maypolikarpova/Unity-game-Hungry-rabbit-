using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController: MonoBehaviour
{
    
    public GameObject lost;
    public int level;
    public bool isGameMode = true;
    Vector3 startingPosition;
    public int coins_quantity;
    public int fruits_quantity;
    public int fruits_total;
    public int maxLifes;
    public int lifes;
    public bool blue;
    public bool red;
    public bool green;
    public static LevelController current = null;

    public UI2DSprite life1;
    public UI2DSprite life2;
    public UI2DSprite life3;
    public Sprite empty;
    public Sprite full;
    public UILabel coins;

    public AudioClip music = null;
    AudioSource musicSource = null;

   
    void Start()
    {
       

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
        
        if (PlayerPrefs.GetInt("coins") == 0) this.coins_quantity = 0;
        else this.coins_quantity = PlayerPrefs.GetInt("coins");

        if (!isGameMode)
        {
            if (LevelController.current.coins_quantity < 10) coins.text = "000" + LevelController.current.coins_quantity.ToString();
            else if (LevelController.current.coins_quantity < 100) coins.text = "00" + LevelController.current.coins_quantity.ToString();
            else if (LevelController.current.coins_quantity < 1000) coins.text = "0" + LevelController.current.coins_quantity.ToString();
            else coins.text = LevelController.current.coins_quantity.ToString();
        }
    }

    public void stopMusic()
    {
        stop = true;
    }

    public void playMusic()
    {
        stop = false;
    }

    bool stop = false;

    void FixedUpdate()
    {
        if (SoundManager.Instance.isMusicOn()&&!musicSource.isPlaying&&!stop)
        {
            musicSource.Play();
        }
        if (!SoundManager.Instance.isMusicOn()||stop)
        {
            musicSource.Pause();
        }

    }

    void Awake()
    {
        current = this;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;

    }

    public Vector3 getStartPosition()
    {
        return this.startingPosition;

    }

    public void onRabitDeath(HeroRabbit rab)
    {
      rab.transform.position = this.getStartPosition();
        if (isGameMode)
        {
            lifes--;
            if (lifes == 2) life3.sprite2D = empty;
            else if (lifes == 1) life2.sprite2D = empty;
            else if (lifes==0)
            {
                life1.sprite2D = empty;
                
            }
            else
            {
                GameObject parent = UICamera.first.transform.parent.gameObject;
                GameObject obj = NGUITools.AddChild(parent, lost);
                Lost popup = obj.GetComponent<Lost>();
                popup.transform.position = parent.transform.position;
            }
        }
    }

    public void addCoins(int number)
    {
        coins_quantity += number;
        PlayerPrefs.SetInt("coins", this.coins_quantity);
        PlayerPrefs.Save();
    }

    public void addFruits(int number,int i)
    {
        fruits_quantity += number;
        collectedFruits.Add(i);
    }

    public void addGems(int number,string type)
    {
        if (type.CompareTo("red") == 0)
        {
            red = true;
        }
        else if (type.CompareTo("blue") == 0)
        {
            blue = true;
        }
        else if (type.CompareTo("green") == 0)
        {
            green = true;
        }
    }
    public void addLife()
    {
        if(lifes==0) life1.sprite2D = full;
        else if(lifes==1) life2.sprite2D = full;
        else life3.sprite2D = full;
        if (lifes<maxLifes)lifes++;
    }

    LevelStats stats;
    public List<int> collectedFruits = new List<int>();


    public void createStats(bool win)
    {   
        string str = PlayerPrefs.GetString("stats" + level.ToString());
        this.stats = JsonUtility.FromJson<LevelStats>(str);

        if (this.stats==null)
        {
            stats = new LevelStats();
            if (blue && red && green) stats.hasAllCrystals = true;
            //if (fruits_quantity == fruits_total) stats.hasAllFruits = true;
            if (stats.collectedFruits.Capacity == fruits_total) stats.hasAllFruits = true;

            if(win)stats.collectedFruits = this.collectedFruits;

            stats.levelPassed = win;

            string str2 = JsonUtility.ToJson(this.stats);
            PlayerPrefs.SetString("stats" + level.ToString(), str2);
            PlayerPrefs.Save();
        }

        else
        {
            if (!this.stats.hasAllCrystals&& blue && red && green)
                stats.hasAllCrystals = true;
            if(!this.stats.hasAllFruits&&stats.collectedFruits.Capacity == fruits_total) 
                stats.hasAllFruits = true;
            if(!this.stats.levelPassed)
                stats.levelPassed = win;

            if(win) stats.collectedFruits.AddRange(collectedFruits);



            string str2 = JsonUtility.ToJson(this.stats);
            PlayerPrefs.SetString("stats" + level.ToString(), str2);
            PlayerPrefs.Save();
        }
        

       
    }

    public LevelStats getStats()
    {
        return this.stats;
    }
   
}
