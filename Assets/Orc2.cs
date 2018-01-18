using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : MonoBehaviour
{
    public GameObject prCarrot;
     public AudioClip attackSound= null;
    AudioSource sound = null;
    public Vector3 moveBy;
    Vector3 pointA;
    Vector3 pointB;
    Rigidbody2D myBody;
    public float speed;
    Vector3 rab;
    SpriteRenderer sr;
    public Animator animator;
    
    bool attack = false;

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        pointA = transform.position;
        pointB = pointA - moveBy;
        sound = gameObject.AddComponent<AudioSource>();

        sound.clip = attackSound;


    }

    void FixedUpdate()
    {
        //this.transform.Translate(speedVect * Time.deltaTime);
        //[-1, 1]
        float value = getDirection(); //velocity

        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

       sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = false;
        }
        else if (value > 0)
        {
            sr.flipX = true;
        }

        animator = GetComponent<Animator>(); // run-idle
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        rab = HeroRabbit.lastRabbit.transform.position;
        if (rab.x > Mathf.Min(pointA.x, pointB.x) && rab.x < Mathf.Max(pointA.x, pointB.x))
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

        if (attack)
        {
            animator.SetBool("attack", true);
            if (SoundManager.Instance.isSoundOn() && !sound.isPlaying)
            {
                sound.loop = true;
                sound.Play();
            }

        }
        else
        {
            animator.SetBool("attack", false);
            sound.Stop();
        }


    }
    float last_carrot;

    void launchCarrot(float dir)
    {
        GameObject ob = GameObject.Instantiate(this.prCarrot);
        ob.transform.position = this.transform.position + Vector3.up*1.5f;
        WeaponCarrot c = ob.GetComponent<WeaponCarrot>();
        c.moveBy = this.moveBy;
        c.launch(dir);
        last_carrot = Time.time;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) <= 0.2f;
    }

    float dir = -1;
    bool toA = true;
    float getDirection()
    {
        Vector3 my = transform.position;
        if (attack&&dead==false)
        {
            if (my.x < rab.x)
            {
                sr.flipX = true;
                if(Time.time - last_carrot> WeaponCarrot.time &&dead== false)  this.launchCarrot(1);
                return 0;
            }
            else
            {
                sr.flipX = false;
                if (Time.time - last_carrot > WeaponCarrot.time &&dead == false) this.launchCarrot(-1);
                return 0;
            }
        }
        else
        {

            if (toA)
            {
                if (isArrived(my, pointB) == false) dir = -1;
                else
                {
                    dir = 1;
                    toA = false;
                }
            }
            if (!toA)
            {
                if (isArrived(my, pointA) == false) dir = 1;
                else
                {
                    dir = -1;
                    toA = true;
                }
            }
        }
        return dir;
    }
    bool dead = false;
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            HeroRabbit r = coll.gameObject.GetComponent<HeroRabbit>();
            Vector3 my = this.transform.position;
            Vector3 it = r.transform.position;

            if (r != null && !r.isGrounded)
            {
                dead = true;
                animator.SetBool("dead",true);
                StartCoroutine(die(1.0f));
            }
            else
            {
                this.animator.SetBool("attack0",true);
                r.dead = true;
                StartCoroutine(rabitDie(1.0f, r));
            }
        }
    }

    IEnumerator die(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }

    IEnumerator rabitDie(float time, HeroRabbit r)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("attack0", false);
        r.dead = false;
        LevelController.current.onRabitDeath(r);
    }
}
