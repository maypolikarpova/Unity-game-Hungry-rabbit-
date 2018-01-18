using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

    public float wait;
    bool isHit = false;
    HeroRabbit r;
    
    public override void OnRabitHit(HeroRabbit rabit)
    {
        if (rabit.health < rabit.MaxHealth)
        {
            r = rabit;
            isHit = true;
            rabit.dead = true;
        }
        else if (rabit.health==rabit.MaxHealth)
        {
            rabit.removeHealth(1);
            rabit.onHealthChange();
            rabit.superPower = true;
            this.CollectedHide();
        }
    }

    void FixedUpdate()
    {
       if(isHit) wait -= Time.deltaTime;
      if (wait < 0)
        {
            r.dead = false;
            LevelController.current.onRabitDeath(r);
            this.CollectedHide();
        }
    }
}
