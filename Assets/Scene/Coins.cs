using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coins : Collectable {
    public UILabel amount;

    public override void OnRabitHit(HeroRabbit rab)
    {
        LevelController.current.addCoins(1);
        if(LevelController.current.coins_quantity   <   10) amount.text = "000" + LevelController.current.coins_quantity.ToString ();
        else if (LevelController.current.coins_quantity < 100) amount.text = "00" + LevelController.current.coins_quantity.ToString();
        else if (LevelController.current.coins_quantity < 1000) amount.text = "0" + LevelController.current.coins_quantity.ToString();
        else amount.text = LevelController.current.coins_quantity.ToString();
        this.CollectedHide();
    }
}
