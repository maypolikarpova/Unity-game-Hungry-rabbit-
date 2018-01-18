using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fruit : Collectable
{
    public UILabel amount;
    public int index;
   
    bool isCollected()
    {
        int level = LevelController.current.level;
        LevelStats cur = JsonUtility.FromJson<LevelStats>(PlayerPrefs.GetString("stats" + level.ToString()));
        if (cur != null && cur.collectedFruits != null && cur.collectedFruits.Contains(index))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Start()
    {
        if (isCollected())
        {
            hide();
            collected = true;
        }
    }

    void hide()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255, 255, 255, 0.5f);
    }

    void collect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255, 255, 255, 0.0f);
        collected = true;
    }

    bool collected = false;
    public override void OnRabitHit(HeroRabbit rab)
    {
        if (!collected)
        {
            LevelController.current.addFruits(1, index);
            amount.text = LevelController.current.fruits_quantity.ToString() + "/" + LevelController.current.fruits_total.ToString();
            collect();
        }
    }
}
