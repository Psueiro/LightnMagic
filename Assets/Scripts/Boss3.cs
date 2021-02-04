using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss3 : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public float Life;
    public float IdleTime = 120;
    public float CastingDuration = 200;
    public float InvulFrames = 60;
    public float DieFrames = 30;
    public float FireballLimit = 0;

    public bool Idle;
    public bool Dashing;
    public bool Blocking;
    public bool Dying;
    public bool DamageFlinch;
    public bool CastingFire;
    public bool CastingIce;
    public bool CastingLightning;
    public bool LookLeft;
    public bool CanGetDamaged;

    public GameObject Jamir;
    public GameObject ORB;
    public GameObject Fireball;
    public GameObject Flake;

    public DifficultyCheck Dif;

    // Use this for initialization
    void Start ()
    {

        LookLeft = true;
        Dif = GameObject.Find("DifficultyChecker").GetComponent<DifficultyCheck>();
            switch (Dif.difficulty)
        {
            case 0:
            Life = 120;
                break;
            case 1:
            Life = 150;
                break;
            case 2:
                    Life = 180;
                break;
        }
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level3")
        {
            SoundManager.instance.Stop(SoundID.LEVEL3);
            SoundManager.instance.Play(SoundID.FINALBOSS, true, 0.3f);
        }

        Idle = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Idle == true)
        {
            anim.ChangeAnimation("Idle");
            IdleTime--;
            Dashing = false;
            CastingFire = false;
            CastingIce = false;
            CastingLightning = false;
            SoundManager.instance.Play(SoundID.WINGS);
        }

    if (Dashing == true)
        {
            Idle = false;
            CastingFire = false;
            CastingIce = false;
            CastingLightning = false;
            if (LookLeft == true)
            {
                Jamir.transform.position -= Jamir.transform.right * 8 * Time.deltaTime;
            }else if (LookLeft == false)
            {
                Jamir.transform.position += Jamir.transform.right * 8 * Time.deltaTime;
            }
            anim.ChangeAnimation("Dash");
            IdleTime = 120;
            SoundManager.instance.Stop(SoundID.WINGS);
        }

        if (Life <= 0)
        {
            Dying = true;
        }

        if (DamageFlinch)
        {
            anim.ChangeAnimation("Hit");
            CanGetDamaged = false;
            Jamir.transform.position += Jamir.transform.right * 0.3f * Time.deltaTime;
            InvulFrames--;
            if (InvulFrames <= 0)
            {
                Idle = true;
                DamageFlinch = false;
                InvulFrames = 60;
                CanGetDamaged = true;
            }
        }

        if (Blocking)
        {
            anim.ChangeAnimation("Block");
            Jamir.transform.position += Jamir.transform.right * 0.3f * Time.deltaTime;
            InvulFrames--;
            if (InvulFrames <= 0)
            {
                Idle = true;
                Blocking = false;
                InvulFrames = 60;
            }
        }

        if (Dying)
        {
            anim.ChangeAnimation("Dead");
            DieFrames--;
            if (DieFrames == 0)
            {
                GameObject Orb = GameObject.Instantiate(ORB);
                Orb.transform.position = this.transform.position;
                Destroy(this.gameObject);

                if (sceneName == "Level3")
                {
                    SoundManager.instance.Stop(SoundID.FINALBOSS);
                    SoundManager.instance.Play(SoundID.VICTORY, true, 0.3f);
                }
            }
        }

        if (CastingFire == true)
        {
            Idle = false;
            Dashing = false;
            CastingIce = false;
            CastingLightning = false;
            anim.ChangeAnimation("Attack");
            CastingDuration--;
            if (FireballLimit < 1)
            {
                GameObject JamirFireball = GameObject.Instantiate(Fireball);
                JamirFireball.transform.position = this.transform.position;
                FireballLimit++;
            }
            
        }
    if (CastingIce == true)
        {
            Idle = false;
            Dashing = false;
            CastingFire = false;
            CastingLightning = false;
            anim.ChangeAnimation("Cast2");
            CastingDuration--;
            GameObject Flakes = GameObject.Instantiate(Flake);
            Flakes.transform.position = this.transform.position;
            SoundManager.instance.Play(SoundID.ICE);
        }
    if (CastingLightning == true)
        {
            Idle = false;
            Dashing = false;
            CastingFire = false;
            CastingIce = false;
            anim.ChangeAnimation("Cast1");
            CastingDuration--;
        }
    if (CastingDuration <= 0)
        {
            CastingFire = false;
            CastingIce = false;
            CastingLightning = false;
            FireballLimit = 0;
            Idle = true;
            IdleTime = 120;
        }
        if (CastingLightning == false && CastingFire == false && CastingIce == false)
        {
            CastingDuration = 200;
        }

	}

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 9)
        {
            if (CastingLightning == true)
            {
                if (CanGetDamaged)
                {
                    Life -= 10;
                }
                DamageFlinch = true;
                SoundManager.instance.Play(SoundID.HIT, false);
            }
            else if (CastingLightning == false)
            {
                Blocking = true;
                SoundManager.instance.Play(SoundID.BLOCK);
            }
        }
        if (c.gameObject.layer == 10)
        {
            if (CastingFire == true)
            {
                if (CanGetDamaged)
                {
                    Life -= 10;
                }

                DamageFlinch = true;
                SoundManager.instance.Play(SoundID.HIT, false);
            }
            else if (CastingFire == false)
            {
                Blocking = true;
                SoundManager.instance.Play(SoundID.BLOCK);
            }
        }
        if (c.gameObject.layer == 11)
        {
            if (CastingIce == true)
            {
                if (CanGetDamaged)
                {
                    Life -= 10;
                }
                DamageFlinch = true;
                SoundManager.instance.Play(SoundID.HIT, false);
            }
            else if (CastingIce == false)
            {
                Blocking = true;
                SoundManager.instance.Play(SoundID.BLOCK);
            }
        }

    }
   }
