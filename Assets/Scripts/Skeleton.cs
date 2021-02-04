using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public float Life;
    public float InvulFrames = 60;
    public float FireDamage = 10;
    public float DieFrames = 60;
    public float StartUpframes = 20;
    public float AttackFrames = 25;
    public float AttackCooldown = 0;
    public float WalkSpeed = 4;

    public float timer;
    public bool Blocking;
    public bool DamageFlinch;
    public bool Dying;
    public bool CanGetDamaged;
    public bool Walking;
    public bool LookLeft;
    public bool Attack;
    public bool electrified;

    public GameObject Skelly;
    public GameObject target;
    public GameObject electricity;

    public DifficultyCheck dif;

    public SpriteRenderer render;

    public ColliderManager colliderManager;

    // Use this for initialization
    void Start()
    {
        dif = GameObject.Find("DifficultyChecker").GetComponent<DifficultyCheck>();
        switch (dif.difficulty)
        {
            case 0:
                Life = 20;
                break;
            case 1:
                Life = 30;
                timer = 4;
                break;
            case 2:
                Life = 50;
                timer = 2;
                break;
        }

        colliderManager = GetComponent<ColliderManager>();

        target = GameObject.Find("Hero");
        CanGetDamaged = true;
        electrified = false;
        Dying = false;
        DamageFlinch = false;
        Blocking = false;
        Walking = false;
        LookLeft = true;
        Attack = false;
        render = GetComponent<SpriteRenderer>();
        colliderManager.ActiveCollider("StabLeft", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dif.difficulty == 1)
        {
            if (electrified) timer -= 1 * Time.deltaTime;
            if(timer < 0)
            {
                ShootLightning();
            }
        }else if (dif.difficulty == 2)
        {
            timer -= 1 * Time.deltaTime;
            if (timer < 0)
            {
                ShootLightning();
            }
        }

        if (Life <= 0)
        {
            Dying = true;
        }

        if (AttackCooldown <= 0)
        {
            AttackCooldown = 0;
        }else AttackCooldown--;

        if (Blocking)
        {
            anim.ChangeAnimation("Block");
            Skelly.transform.position += Skelly.transform.right * 0.3f * Time.deltaTime;
            CanGetDamaged = false;
            InvulFrames--;
            Walking = false;
            if (InvulFrames == 0)
            {
                anim.ChangeAnimation("Idle");
                Blocking = false;
                InvulFrames = 60;
                CanGetDamaged = true;
                Walking = true;
            }
        }

        if (Attack)
        {
            Walking = false;
            anim.ChangeAnimation("Stab");
            StartUpframes--;
                if(StartUpframes <= 0)
            {
                ActivateHitCollider();                
            }           
            AttackFrames--;
            AttackCooldown = 60;            
        }        

        if (Skelly.transform.localScale == new Vector3(6, 6, 0) && transform.position.x <= target.transform.position.x + 3 && transform.position.x > target.transform.position.x && AttackCooldown == 0 && transform.position.y >= target.transform.position.y -2 && transform.position.y <= target.transform.position.y + 2 && Attack == false)
        {
            Attack = true;
            SoundManager.instance.Play(SoundID.CUT, false);
        }
        else if (Skelly.transform.localScale == new Vector3(-6, 6, 0) && transform.position.x >= target.transform.position.x - 3 && transform.position.x < target.transform.position.x && AttackCooldown == 0 && transform.position.y >= target.transform.position.y - 2 && transform.position.y <= target.transform.position.y + 2 && Attack == false)
        {
            Attack = true;
            SoundManager.instance.Play(SoundID.CUT, false);
        }

            if (AttackFrames <= 0 && anim.frame == 4)
        {                       
            Attack = false;
            Walking = true;
            StartUpframes = 20;
            AttackFrames = 25;
            DeactivateHitCollider();
            WalkSpeed = 4;            
        }

        if (Walking)
        {   
            anim.ChangeAnimation("Run");
            if(DamageFlinch == false && Blocking == false && Dying == false && LookLeft == true)
            {
                    Skelly.transform.position -= Skelly.transform.right * WalkSpeed * Time.deltaTime;                
            }else if (DamageFlinch == false && Blocking == false && Dying == false && LookLeft == false)
            {
                Skelly.transform.position += Skelly.transform.right * WalkSpeed * Time.deltaTime;
            }
        }

        if (DamageFlinch)
        {
            anim.ChangeAnimation("Damage");
            Skelly.transform.position += Skelly.transform.right * 0.3f * Time.deltaTime;
            InvulFrames--;
            DeactivateHitCollider();            
            if (InvulFrames == 0)
            {
                anim.ChangeAnimation("Idle");
                DamageFlinch = false;
                InvulFrames = 60;
                CanGetDamaged = true;
            }
        }
        if (Dying)
        {
            anim.ChangeAnimation("Dead");
            DieFrames--;
            CanGetDamaged = false;
            DeactivateHitCollider();
                if (DieFrames == 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (Skelly.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        { 
            Walking = true;             
        }
        if (c.gameObject.layer == 9)
        {
            if (Dying == false)
            {
                if (CanGetDamaged)
                {
                    Life -= FireDamage;
                    CanGetDamaged = false;
                    WalkSpeed = 4;
                    SoundManager.instance.Play(SoundID.HIT);
                }
                DamageFlinch = true;
            }
        }
        if (c.gameObject.layer == 10)
        {
            Blocking = true;
            if (WalkSpeed >= 0.25f)
            {
                WalkSpeed = WalkSpeed * 0.5f;
            }
            SoundManager.instance.Play(SoundID.BLOCK);
        }
        if (c.gameObject.layer == 11)
        {
            Blocking = true;
            electrified = true;
            if (WalkSpeed <= 15)
            {
                WalkSpeed = WalkSpeed * 1.5f;
                if (WalkSpeed > 15)
                {
                    WalkSpeed = 15;
                }
            }
            SoundManager.instance.Play(SoundID.BLOCK);
        }
        if (c.gameObject.layer == 15)
        {
            if (LookLeft == true && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(-6, 6, 0);
                LookLeft = false;
            }
            else if (LookLeft == false && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(6, 6, 0);
                LookLeft = true;
            }
        }

        if (c.gameObject.layer == 17)
        {
            if (LookLeft == true && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(-6, 6, 0);
                LookLeft = false;
            }
            else if (LookLeft == false && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(6, 6, 0);
                LookLeft = true;
            }
        }

        if (c.gameObject.layer == 8)
        {
            WalkSpeed = 0;
        }
    }

    void OnCollisionExit2D(Collision2D d)
    {
        if (d.gameObject.layer == 8)
        {
            WalkSpeed = 4;
        }
        if (d.gameObject.layer == 14)
        {
            WalkSpeed = 4;
        }
        if (d.gameObject.layer == 13)
        {
            WalkSpeed = 4;
        }
    }

    void OnCollisionStay2D(Collision2D e)
    {
        if (e.gameObject.layer == 15)
        {
            if (LookLeft == true && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(-6, 6, 0);
                LookLeft = false;
            }
            else if (LookLeft == false && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(6, 6, 0);
                LookLeft = true;
            }
        }

        if (e.gameObject.layer == 17)
        {
            if (LookLeft == true && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(-6, 6, 0);
                LookLeft = false;
            }
            else if (LookLeft == false && Attack == false)
            {
                Skelly.transform.localScale = new Vector3(6, 6, 0);
                LookLeft = true;
            }
        }
    }

    public void ShootLightning()
    {
        Attack = true;
        GameObject newLightning;
        if (anim.currentAnimation.name == "Stab" && anim.frame == 3)
        { 
        newLightning = Instantiate(electricity);
        Vector2 litSpawn;
        litSpawn = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        newLightning.transform.position = litSpawn;
        Skelightning skl;
        skl = newLightning.GetComponent<Skelightning>();
        if (LookLeft) skl.goRight = false; else skl.goRight = true;
            if (dif.difficulty == 1) timer = 4; else if (dif.difficulty == 2) timer = 2;
        }
    }

    public void ActivateHitCollider()
    {
        if (!render.flipX) colliderManager.ActiveCollider("StabLeft", true);
    }
    public void DeactivateHitCollider()
    {
        colliderManager.ActiveCollider("StabLeft", false);
    }
}