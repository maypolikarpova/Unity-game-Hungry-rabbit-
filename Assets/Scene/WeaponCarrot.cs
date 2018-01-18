using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarrot : Collectable
{

    public static float time = 2;
    public Vector3 moveBy;
    public float speed;


    void Start()
    {
        StartCoroutine(destroyLater(time));

    }

    IEnumerator destroyLater(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    public void launch(float dir)
    {
        moveBy = moveBy * dir;

        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (dir < 0) sr.flipX = true;
        else sr.flipX = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(moveBy * Time.deltaTime);
    }

    public override void OnRabitHit(HeroRabbit rabit)
    {
        rabit.dead = true;
        StartCoroutine(die(1.0f,rabit));
    }

    IEnumerator die(float time,HeroRabbit r)
    {
        yield return new WaitForSeconds(time);
        r.dead = false;
        LevelController.current.onRabitDeath(r);
    }


}
