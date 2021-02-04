using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Boss2 : MonoBehaviour
{

    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public float Life;
    public float IdleTime = 120;
    public float SlashLimited = 0;
    public float SlashAnimation = 30;
    public float JumpFrames = 60;
    public float InvulFrames = 60;
    public float DieFrames = 30;

    public bool LookLeft;
    public bool Idle;
    public bool Blocking;
    public bool Dying;
    public bool Dashing;
    public bool DamageFlinch;
    public bool Attacking;
    public bool Jumping;
    public bool CanGetDamaged;
    public bool Submerged;

    public LayerMask MyLayerMask;

    public GameObject Undyne;
    public GameObject ORB;
    public GameObject Slash1;
    public GameObject Slash2;

    public DifficultyCheck Dif;
    

    // Use this for initialization
    void Start()
    {
        Dif = GameObject.Find("DifficultyChecker").GetComponent<DifficultyCheck>();
       
        switch (Dif.difficulty)
       {
           case 0:
           Life = 80;
               break;
           case 1:
           Life = 100;
               break;
           case 2:
           Life = 120;
               break;
       }

        LookLeft = true;
        Idle = true;
        Dashing = false;
        Dying = false;
        Blocking = false;
        DamageFlinch = false;
        Jumping = false;
        CanGetDamaged = true;
        Submerged = false;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level2")
        {
            SoundManager.instance.Stop(SoundID.LEVEL2);
            SoundManager.instance.Play(SoundID.BOSS, true, 0.3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Idle == true)
        {
            anim.ChangeAnimation("Idle");
            IdleTime--;
            SlashAnimation = 30;
            JumpFrames = 60;
        }
        if (Attacking == true)
        {
            Idle = false;
            SlashAnimation--;
        }
        if (SlashAnimation <= 0)
        {
            Idle = true;
            Attacking = false;
        }

        if (Life <= 0)
        {
            Dying = true;
        }

        if (Blocking)
        {
            anim.ChangeAnimation("Block");
            Undyne.transform.position += Undyne.transform.right * 0.3f * Time.deltaTime;
            CanGetDamaged = false;
            InvulFrames--;
            if (InvulFrames <= 0)
            {
                Idle = true;
                Blocking = false;
                InvulFrames = 60;
                CanGetDamaged = true;
            }
        }

        if (DamageFlinch)
        {
            anim.ChangeAnimation("Hit");
            Undyne.transform.position += Undyne.transform.right * 0.3f * Time.deltaTime;
            InvulFrames--;
            if (InvulFrames <= 0)
            {
                Idle = true;
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
            if (DieFrames == 0)
            {
                GameObject Orb = GameObject.Instantiate(ORB);
                Orb.transform.position = this.transform.position;
                Destroy(this.gameObject);

                if (sceneName == "Level2")
                {
                    SoundManager.instance.Stop(SoundID.BOSS);
                    SoundManager.instance.Play(SoundID.VICTORY, true, 0.3f);
                }
            }
        }

        if (Jumping)
        {
            JumpFrames--;
        }

        if (Dashing)
        {
            Dash();
        }

        Vector2 rayPos;
        rayPos = new Vector2(this.transform.position.x, this.transform.position.y + 1);

        RaycastHit2D checkwater;
        checkwater = Physics2D.Raycast(rayPos, Vector2.right , 1, MyLayerMask);
        Debug.DrawRay(rayPos, Vector2.right, Color.green);
        if(!checkwater.collider.gameObject)
        {
            Submerged = false; 
        }
        else Submerged = true;
    }

    public void ResetCooldowns()
    {
        SlashLimited = 0;
    }


    public void SlashingUp()
    {
            Idle = false;
            IdleTime = 120;

        if(SlashAnimation > 0)
        {
            anim.ChangeAnimation("Attack1");
        }
            
        if (SlashLimited < 1)
        {
            Attacking = true;       
            GameObject SlashUp = GameObject.Instantiate(Slash1);
            SlashUp.transform.position = this.transform.position;
            SlashLimited++;
            SoundManager.instance.Play(SoundID.CUT, false);
        }       
    }

    public void Dash()
    {
            Idle = false;
            Jumping = false;
            Blocking = false;
            Attacking = false;
            CanGetDamaged = false;
            anim.ChangeAnimation("Dash");
            if (LookLeft == true)
        {
            Undyne.transform.position -= Undyne.transform.right * 8 * Time.deltaTime;
        }else if( LookLeft == false)
        {
            Undyne.transform.position += Undyne.transform.right * 8 * Time.deltaTime;
        }
            
            IdleTime = 120;      
    }

    public void Jump()
    {
        Idle = false;
        Jumping = true;
        Attacking = false;
        Blocking = false;
        SlashLimited = 0;
        anim.ChangeAnimation("Attack1");
        rb.AddForce(Vector3.up * 12, ForceMode2D.Impulse);
        IdleTime = 120;
    }
    public void SlashingDown()
    {
        Idle = false;
        Jumping = false;                                                                                                                                                                                                                    
        Blocking = false;
        Dashing = false;
        IdleTime = 120;
        if (SlashAnimation > 0)
        {
            anim.ChangeAnimation("Attack1");
        }
        anim.ChangeAnimation("Attack2");
        Attacking = true;
        if (SlashLimited < 1 && Dashing == false)
        {
            GameObject SlashDown = GameObject.Instantiate(Slash2);
            SlashDown.transform.position = this.transform.position;
            SlashLimited++;
            SoundManager.instance.Play(SoundID.CUT, false);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {   
            Idle = true;           
            Jumping = false;
            SoundManager.instance.Play(SoundID.LAND);
        }
            if (c.gameObject.layer == 9)
        {
            if(CanGetDamaged == true && Submerged == false && Dying == false)
            {
                Life -= 10;
                CanGetDamaged = false;
                SoundManager.instance.Play(SoundID.HIT, false);
                DamageFlinch = true;
            }
            
        }

        if (c.gameObject.layer == 10)
        {
            Blocking = true;
            SoundManager.instance.Play(SoundID.BLOCK);
        }

        if (c.gameObject.layer == 11)
        {
            if (CanGetDamaged == true && Submerged == true && Dying == false)
            {
                Life -= 10;
                CanGetDamaged = false;
                SoundManager.instance.Play(SoundID.HIT, false);
                DamageFlinch = true;
            }
        }
        if (c.gameObject.layer == 15)
        {
            Dashing = false;
            Idle = true;
        }

    }
}
