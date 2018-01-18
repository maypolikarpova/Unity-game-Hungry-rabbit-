using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{
    public UI2DSprite gem;
    public Sprite gem2;

    public string type;// blue, red, green

    public override void OnRabitHit(HeroRabbit rab)
    {
        LevelController.current.addGems(1,type);
        gem.sprite2D = gem2;
        this.CollectedHide();
    }
}
