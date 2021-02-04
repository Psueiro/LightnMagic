using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;

    public float Life = 5;
    public float Heat = 0;
    public float DieFrames = 60;
    public float BlockFrames = 60;
    public float WalkSpeed = 2;
    public float InvulFrames = 60;
    public float IdleTime = 40;
    public float timer;

    public int mask = 1 << 19;

    public bool CanGetDamaged;
    public bool IsHit;
    public bool Blocking;
    public bool Walking;
    public bool DamageFlinch;
    public bool Dying;
    public bool IsGrounded;
    public bool Hitting;
    public bool LookLeft;

    public SpriteRenderer render;

    public Color HottestColor;
    public Color HotColor;
    public Color NormalColor;
    public Color ChillColor;

    public GameObject Golem;
    public GameObject ORB;
    public GameObject Rock;

    public DifficultyCheck dif;


    // Use this for initialization
    void Start()
    {
         dif = GameObject.Find("DifficultyChecker").GetComponent<DifficultyCheck>();
        switch (dif.difficulty)
        {    
           case 0:
           Life = 3;
               break;
           case 1:
           Life = 5;
               break;
           case 2:
              Life = 7;
           break;
        }

        if (dif.difficulty == 1) timer = 5; else if (dif.difficulty == 2) timer = 3;

        Dying = false;
        Walking = false;
        Blocking = false;
        DamageFlinch = false;
        IsGrounded = false;
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level")
        {
            SoundManager.instance.Stop(SoundID.LEVEL1);
            SoundManager.instance.Play(SoundID.BOSS, true, 0.3f);
        }
    }


    // Update is called once per frame
    void Update()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (dif.difficulty > 0)
        {
            timer -= 1 * Time.deltaTime;
            if (timer < 0) SpawnRocks();
        }

        if (Life == 0)
        {
            Dying = true;
        }

        if (Walking)
        {
                anim.ChangeAnimation("Walking");
                if (LookLeft)
            {
                Golem.transform.position -= Golem.transform.right * WalkSpeed * Time.deltaTime;
            }else Golem.transform.position += Golem.transform.right * WalkSpeed * Time.deltaTime;

            if (anim.frame == 3)
                {
                    WalkSpeed = 0;
                SoundManager.instance.Play(SoundID.GOLEMWALK);
            }
                else if (anim.frame == 9)
                {
                    WalkSpeed = 0;
                SoundManager.instance.Play(SoundID.GOLEMWALK);
            }           
        }

        if (WalkSpeed == 0)
        {
            IdleTime--;
        }

        /*        if (Walking == false && DamageFlinch == false && Dying == false)
                {
                    anim.ChangeAnimation("Idle");
                    IdleTime--;
                }*/

        if (IdleTime <= 0 && DamageFlinch == false)
        {
            //Walking = true;
            WalkSpeed = 2;
            IdleTime = 20;
        }

        if (Blocking)
        {
            anim.ChangeAnimation("Block");
            if (LookLeft)
            {
                Golem.transform.position += Golem.transform.right * 0.3f * Time.deltaTime;
            }else Golem.transform.position -= Golem.transform.right * 0.3f * Time.deltaTime;
            BlockFrames--;
            Walking = false;
            SoundManager.instance.Play(SoundID.BLOCK);
            if (BlockFrames == 0)
            {
                anim.ChangeAnimation("Idle");
                Blocking = false;
                BlockFrames = 60;
                Walking = true;                
            }
        }

        if (DamageFlinch)
        {
            anim.ChangeAnimation("Hit");
            if (LookLeft)
            {
                Golem.transform.position += Golem.transform.right * 0.3f * Time.deltaTime;
            }else Golem.transform.position -= Golem.transform.right * 0.3f * Time.deltaTime;
            Walking = false;
            InvulFrames--;
            Heat = 0;
            if (InvulFrames == 0)
            {
                anim.ChangeAnimation("Idle");
                DamageFlinch = false;
                InvulFrames = 60;
                CanGetDamaged = true;
                Walking = true;
            }
        }

        if (Hitting)
        {
            anim.ChangeAnimation("Attack");
            if (anim.frame == 7)
            {
                Hitting = false;
                Walking = true;
            }
        }


        if (Golem.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }

        if (Dying)
        {
            anim.ChangeAnimation("Dead");            
            DieFrames--;
            CanGetDamaged = false;

            if (DieFrames == 0)
            {
                
                Destroy(this.gameObject);                

                if (sceneName == "Level")
                {
                    GameObject Orb = GameObject.Instantiate(ORB);
                    Orb.transform.position = this.transform.position;
                    SoundManager.instance.Stop(SoundID.BOSS);
                    SoundManager.instance.Play(SoundID.VICTORY, true, 0.3f);
                }
            }
        }

    }

    public void SpawnRocks()
    {
        if(SceneManager.GetActiveScene().name == "Level")
        {
            GameObject newRock;
            newRock = Instantiate(Rock);
            newRock.transform.position = new Vector2(this.transform.position.x + Random.Range(-8,8), 6);
            if (dif.difficulty == 1) timer = 5; else if (dif.difficulty == 2) timer = 3;
            Destroy(newRock, 2);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0 && Dying == false)
        {
            Walking = true;
            IsGrounded = true;
            SoundManager.instance.Play(SoundID.GOLEMWALK);
        }

        if (c.gameObject.layer == 9 && Dying == false)
        {
            if (Heat < 25)
            {
                Heat++;
                this.gameObject.GetComponent<SpriteRenderer>().color = HotColor;
            }
            if (Heat == 25)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = HottestColor;
            }
        }

        if (c.gameObject.layer == 10 && Dying == false)
        {

            if (Heat < 25)
            {
                Blocking = true;
                Heat = 0;
                this.gameObject.GetComponent<SpriteRenderer>().color = NormalColor;
            }
            else DamageFlinch = true;
            this.gameObject.GetComponent<SpriteRenderer>().color = ChillColor;
        }
        if (c.gameObject.layer == 11 && Dying == false)
        {
            if (DamageFlinch)
            {
                Life--;
                this.gameObject.GetComponent<SpriteRenderer>().color = NormalColor;
                if (Life <= 0)
                {
                    Dying = true;
                }
            }
            else Blocking = true;
        }

    }

}

