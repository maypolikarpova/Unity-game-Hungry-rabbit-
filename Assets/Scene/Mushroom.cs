using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable {

    public override void OnRabitHit(HeroRabbit rabit)
    {
        rabit.addHealth(1);
        rabit.onHealthChange();
        this.CollectedHide();
    }
}
