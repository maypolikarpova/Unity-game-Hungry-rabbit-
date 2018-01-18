using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable
{

    public override void OnRabitHit(HeroRabbit rab)
    {
        LevelController.current.addLife();
        this.CollectedHide();
    }
}
