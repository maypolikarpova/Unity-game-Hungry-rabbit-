using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public static HeroRabbit lastRabbit = null;

    public AudioClip run= null;
    public AudioClip jump = null;
    public AudioClip death = null;
    AudioSource sound = null;

    public float speed = 1;
    Rigidbody2D myBody = null;
   
    public bool isGrounded = false;//jump
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    bool isHit = false;
    public float wait = 1;


    public bool dead = false; // PUBLIC
    public bool superPower = false;

    Transform heroParent = null;

    void Awake()
    {
        lastRabbit = this;
    }
    // Use this for initialization
    void Start () {
        
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);

        this.heroParent = this.transform.parent;

        sound = gameObject.AddComponent<AudioSource>();
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //[-1, 1]
        float value = Input.GetAxis("Horizontal"); //velocity
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }

        Animator animator = GetComponent<Animator>(); // run-idle
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
            if (SoundManager.Instance.isSoundOn()&&!sound.isPlaying)
            {
                sound.clip = run;
                sound.loop = true;
                sound.Play();
            }
        }
        else
        {
            animator.SetBool("run", false);
            sound.Stop();
        }

        // death
      
        if (dead)
        {
            animator.SetBool("die", true);
            if (SoundManager.Instance.isSoundOn())
            {
                sound.clip = death;
                sound.Play();
            }
        }
        else
        {

            animator.SetBool("die", false);
        }

      


        //jump
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime) + 0.7f;
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }

        //jump animation
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
            
            if (SoundManager.Instance.isSoundOn())
            {
                sound.clip = jump;
                sound.Play();
            }
        }


        if (hit)//platform
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {

                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }



        //super power after bomb

        if (superPower)
        {
            ps -= Time.deltaTime;
            health = 1000;
            sr.color = new Color(255,0,0);
        }

        if (ps<0)
        {
            superPower = false;
            health = 1;
            sr.color = new Color(255, 255, 255);
        }


        if (isHit) wait -= Time.deltaTime;
        if (wait < 0)
        {
            this.dead = false;
            isHit = false;
            wait = 1;
            LevelController.current.onRabitDeath(this);

        }
    }
    float ps = 4;
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }
    public int health = 1;
    public int MaxHealth = 2;

    public void addHealth(int numb)
    {
        this.health += numb;
        if (this.health > MaxHealth)
        {
            this.health = MaxHealth;
        }
        this.onHealthChange();
    }

    public void removeHealth(int numb)
    {
        this.health -= numb;
        if (this.health < 0)
        {
            this.health = 0;
        }
        this.onHealthChange();
    }

    public void onHealthChange()
    {
        if (this.health == 1)
        {
            this.transform.localScale = Vector3.one;
        }
        else if (this.health == 2)
        {
            this.transform.localScale = Vector3.one * 2;
        }
        else if (this.health == 0)
        {
            LevelController.current.onRabitDeath(this);
        }
    }
 



}
