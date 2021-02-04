using UnityEngine;
using System.Collections;

public class Gargoyle : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public float Life;
    public float Flyspeed = 4;
    public float InvulFrames = 60;
    public float FireDamage = 1;
    public float DieFrames = 60;
    public float LightningDamage = 8;

    public int maxTimer;
    public float timer;

    public bool Blocking;
    public bool DamageFlinch;
    public bool Dying;
    public bool CanGetDamaged;
    public bool Flying;
    public bool LookLeft;
    public bool FlyDown;
    public bool Attack;

    public GameObject Goyle;
    public GameObject target;
    public GameObject Shield;

    public DifficultyCheck dif;

    // Use this for initialization
    void Start ()
    {
         dif = GameObject.Find("DifficultyChecker").GetComponent<DifficultyCheck>();
        switch (dif.difficulty)
     {
         case 0:
                Life = 16;
             break;
         case 1:
                Life = 24;
                maxTimer = 3;
             break;
         case 2:
                Life = 32;
                maxTimer = 1;
             break;
     }
        timer = maxTimer;
        CanGetDamaged = true;
        target = GameObject.Find("Hero");
        Flying = true;
        Shield = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        if(Flying)
        { 
            if(DamageFlinch == false && Blocking == false && Dying == false && LookLeft == true)
            {
                Goyle.transform.position -= Goyle.transform.right * Flyspeed * Time.deltaTime;
            }else if (DamageFlinch == false && Blocking == false && Dying == false && LookLeft == false)
            {
                Goyle.transform.position += Goyle.transform.right * Flyspeed * Time.deltaTime;
            }
            if (DamageFlinch == false && Blocking == false && Dying == false && FlyDown == true)
            {
                Goyle.transform.position -= Goyle.transform.up * Flyspeed * Time.deltaTime;
            }else if (DamageFlinch == false && Blocking == false && Dying == false && FlyDown == false)
            {
                Goyle.transform.position += Goyle.transform.up * Flyspeed * Time.deltaTime;
            }
            SoundManager.instance.Play(SoundID.WINGS, true);
        }

        if (dif.difficulty > 0)
        {
            if (timer < 0)
            { Shield.SetActive(!Shield.activeSelf); timer = maxTimer; }
            else timer -= 1 * Time.deltaTime;
            if (Shield.activeSelf) CanGetDamaged = false; else CanGetDamaged = true;
        }
        else Shield.SetActive(false);


        if (transform.position.x < target.transform.position.x - 3)
        {
            transform.localScale = new Vector3(-5, 5, 0);
            LookLeft = false;
        } else if(transform.position.x > target.transform.position.x + 3)
        {
            transform.localScale = new Vector3(5, 5, 0);
            LookLeft = true;
        }

        if (transform.position.y < target.transform.position.y - 1)
        {
            FlyDown = false;
        } else if (transform.position.y > target.transform.position.y + 1)
        {
            FlyDown = true;
        }


            if (Life <= 0)
        {
            Dying = true;
        }

        if (DamageFlinch)
        {
            anim.ChangeAnimation("Damage");
            Goyle.transform.position += Goyle.transform.right * 0.3f * Time.deltaTime;
            InvulFrames--;
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
            Flying = false;
            Goyle.transform.position -= Goyle.transform.up * Flyspeed * 5 * Time.deltaTime;
            if (DieFrames == 0)
            {
                SoundManager.instance.Stop(SoundID.WINGS);
                Destroy(this.gameObject);
            }
        }

        if (Blocking)
        {
            anim.ChangeAnimation("Block");
            Goyle.transform.position += Goyle.transform.right * 0.3f * Time.deltaTime;
            CanGetDamaged = false;
            InvulFrames--;
            Flying = false;
            if (InvulFrames == 0)
            {
                anim.ChangeAnimation("Idle");
                Blocking = false;
                InvulFrames = 60;
                CanGetDamaged = true;
                Flying = true;
            }
          }
        }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 9)
        {
            if (Dying == false)
            {
                if (CanGetDamaged)
                {
                    Life -= FireDamage;
                    CanGetDamaged = false;
                    SoundManager.instance.Play(SoundID.HIT);
                }
                if (!Shield.activeSelf)DamageFlinch = true;
            }
        }

        if (c.gameObject.layer == 10)
        {
            Blocking = true;
            SoundManager.instance.Play(SoundID.BLOCK);
        }

        if (c.gameObject.layer == 11)
        {
            if (CanGetDamaged)
            {
                Life -= LightningDamage;
                CanGetDamaged = false;
                SoundManager.instance.Play(SoundID.HIT);
            }
            if(!Shield.activeSelf)DamageFlinch = true;
        }
    }
}
