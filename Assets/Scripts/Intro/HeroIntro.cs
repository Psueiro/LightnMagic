using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HeroIntro : MonoBehaviour
{
    public float timer1;
    public float timer2;
    public float timer3;

    public float timerOverall;
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public bool gotHit;

	// Use this for initialization
	void Start ()
    {
        timer1 = 2;
        timer2 = 5;
        timer3 = 2;
        anim = this.GetComponent<AnimationControllerCustom>();
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timerOverall > 11.5f) ChangeScene(); 

        timerOverall += 1 * Time.deltaTime;
        if (timer1 > 0) Stage1();
        else if (timer1 <= 0 && timer2 > 0) Stage2();
        else if (timer2 <= 0 && timer3 > 0) Stage3();
        else Stage1();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Stage1()
    {
        timer1 -= 1 * Time.deltaTime;
        anim.ChangeAnimation("Run");
        this.transform.position  += this.transform.right * 5 * Time.deltaTime;
    }

    public void Stage2()
    {
        timer2 -= 1 * Time.deltaTime;
        anim.ChangeAnimation("Hit");
        if(!gotHit)rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        gotHit = true;
    }

    public void Stage3()
    {
        anim.ChangeAnimation("Idle");
        timer3 -= 1 * Time.deltaTime;
        if (timer3 > 1.5 && timer3 < 2 || timer3 < 1 && timer3 > 0.5f )
        {
            this.transform.localScale = new Vector3(-7, 7);
        }
        else this.transform.localScale = new Vector3(7, 7);
    }

}
