using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mage : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;

    public bool canJump;
    public bool canRun;
    public bool Ducking;
    public bool Dead;
    public bool LookLeft;
    public bool Jumping;
    public bool Casting;
    public bool Running;
    public bool Flinching;
    public bool CanFireAppear;
    public bool CanIceAppear;
    public bool CanGetDamaged;    

    public GameObject Hero;
    public GameObject FireSpell;
    public GameObject IceSpell;
    public GameObject LightningSpell;

    public float Life;
    public float MaxLife;
    public float Lives;
    public float FireAmount = 0;
    public float FireCastCooldown = 0.01f;
    public float IceAmount = 0;
    public float IceTime = 180;
    public float LightningAmount = 0;
    public float InvulFrames = 60;
    public float SpellMode = 1;
    //1=Fire  2= ice  3= Lightning
    public float MoveSpeed = 5;
    public Image lifeBar;
    public Image spellIcon;
    public Vector3 originalScale;



    void Start()
    {
        Jumping = false;
        Casting = false;
        Running = false;
        Ducking = false;
        anim = this.GetComponent<AnimationControllerCustom>();
        MaxLife = 50;
        Lives = 1;
        Life = MaxLife;
        lifeBar = GameObject.Find("Canvas").transform.Find("Lifebar").GetComponent<Image>();
        //lifeBar.transform.parent = GameObject.Find("Canvas").transform;
        spellIcon = GameObject.Find("Canvas").transform.Find("SpellIcon").GetComponent<Image>();
       // spellIcon.transform.parent = GameObject.Find("Canvas").transform;
        originalScale = this.transform.localScale;
    }

    void Update()
    {
        lifeBar.fillAmount = Life * 1 / MaxLife;

        if (Life <= 0)
        {
            Dead = true;
        }
        if (Dead)
        {
            Destroy(this.gameObject);
            Lives--;
            if(Lives > 0)
            {
                Life = MaxLife;
            }           
        }
        if (Lives == 0)
        {
            SceneManager.LoadScene("Intro");
        }

        if (Hero.transform.position.y <= -20)
        {
            Dead = true;            
        }

        if (Jumping)
        {
            anim.ChangeAnimation("Jump");
            canJump = false;
        }

        if (Casting)
        {
            if (Running == false)
            {
                anim.ChangeAnimation("Cast");
            }
           
            if (SpellMode == 1)
            {
                
                if (FireAmount < 10)
                {
                    CastFire();
                    FireAmount++;
                }
            }
            if (SpellMode == 2)
            {
                if (IceAmount < 1 && CanIceAppear == true)
                {
                    CastIce();
                    IceAmount++;                
                }
                if (IceTime > 0)
                {
                    IceTime--;
                }
            }
            if (SpellMode == 3)
            {
                if (LightningAmount < 1)
                {
                    CastLightning();
                    LightningAmount++;
                }                
            }
        } else if (Casting == false)
        {
            FireAmount = 0;
        }

        if (Running && canRun)
        {
            if (Jumping == false && Ducking == false)
            {
                anim.ChangeAnimation("Run");
            }

            if (LookLeft)
            {
                Hero.transform.position -= Hero.transform.right * MoveSpeed * Time.deltaTime;
            }
            else if (LookLeft == false)
            {
                Hero.transform.position += Hero.transform.right * MoveSpeed * Time.deltaTime;
            }
        }

        if (Ducking)
        {
            anim.ChangeAnimation("Duck");
            //reducir el hitbox
            canRun = false;
        } else canRun = true;

        if (Flinching)
        {
            anim.ChangeAnimation("Hit");
            InvulFrames--;
            Hero.transform.position += -Hero.transform.right * 0.3f * Time.deltaTime;
            canRun = false;
            canJump = false;
            SoundManager.instance.Play(SoundID.HIT, false);
            if (InvulFrames == 0)
            {
                anim.ChangeAnimation("Idle");
                Flinching = false;
                InvulFrames = 60;
                CanGetDamaged = true;
                canRun = true;
                canJump = true;
            }
        }

        if (Running == false && Jumping == false && Casting == false && Ducking == false && Flinching == false)
        {
            anim.ChangeAnimation("Idle");
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0 || c.collider.gameObject.layer == 0)
        {
            anim.ChangeAnimation("Idle");
            canJump = true;
            Jumping = false;
            SoundManager.instance.Play(SoundID.LAND);
        }

        if (c.gameObject.layer == 12)
        {
            if (CanGetDamaged)
            {
                Life -= 10;
                CanGetDamaged = false;
            }
            Flinching = true;
        }

        if (c.gameObject.layer == 13)
        {

            if (CanGetDamaged)
            {
                Life -= 5;
                CanGetDamaged = false;
            }
            Flinching = true;
        }

        if (c.gameObject.layer == 14)
        {
            if (CanGetDamaged)
            {
                Life -= 15;
                CanGetDamaged = false;
            }
            Flinching = true;
        }
        if (c.gameObject.layer == 18)
        {
            if (CanGetDamaged)
            {
                Life -= 10;
                CanGetDamaged = false;
            }
            Flinching = true;
        }

        if (c.gameObject.layer == 19)
        {
            if (CanGetDamaged)
            {
                Life -= 12;
                CanGetDamaged = false;
            }
            Flinching = true;
        }
    }

    void OnCollisionExit2D(Collision2D g)
    {
        if (g.gameObject.layer == 0 )
        {
            canJump = false;
        }
    }
    void OnCollisionStay2D(Collision2D e)
    {
        if (e.gameObject.layer == 0)
        {
            canJump = true;
            rb.gravityScale = 1;
            MoveSpeed = 5;
        }
    }

    void OnTriggerEnter2D(Collider2D d)
    {
        if (d.gameObject.layer == 4)
        {
            rb.gravityScale = 0.5f;
            MoveSpeed = 2;
        }
    }

    void OnTriggerStay2D(Collider2D d)
    {
        
        if (d.gameObject.layer == 4)
        {
            rb.gravityScale = 0.5f;
            MoveSpeed = 2;
        }else
        {
            rb.gravityScale = 1;
            MoveSpeed = 5;
        }
        /*if(rb.velocity.y < -5)
        {
            rb.velocity = new Vector3(rb.velocity.x, -6);
        }*/
    }

    void OnTriggerExit2D(Collider2D f)
    {
        if (f.gameObject.layer == 4)
        {
            rb.gravityScale = 1;
            MoveSpeed = 5;
        }
    }

    public void CastFire()
    {
            if (CanFireAppear)
            {
                Vector3 mousePositioInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePositioInWorld.z = 0;
                GameObject obj = Instantiate(FireSpell);
                Fire fire = obj.GetComponent<Fire>();
                fire.SetPosition(mousePositioInWorld);
                CanFireAppear = false;
                Invoke("FireCanAppear", FireCastCooldown);
                SoundManager.instance.Play(SoundID.FIRE, false, 0.5f);
        }       
    }

    public void FireCanAppear()
    {
        CanFireAppear = true;
    }


    public void CastIce()
    {
        Vector3 mousePositioInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositioInWorld.z = 0;
        GameObject obj = Instantiate(IceSpell);
        Ice ice = obj.GetComponent<Ice>();
        ice.SetPosition(mousePositioInWorld);
    }

    public void CastLightning()
    {
        Vector3 mousePositioInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositioInWorld.y = 5;
        mousePositioInWorld.z = 0;
        GameObject obj = Instantiate(LightningSpell);
        Lightning lightning = obj.GetComponent<Lightning>();
        lightning.SetPosition(mousePositioInWorld);
        SoundManager.instance.Play(SoundID.LIGHTNING);
    }

}
