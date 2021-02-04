using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndineIntro : MonoBehaviour
{
    public float timer1;
    public float timer2;
    public AnimationControllerCustom anim;

	// Use this for initialization
	void Start ()
    {
        anim = this.GetComponent<AnimationControllerCustom>();
        timer1 = 3;
        timer2 = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timer1 < 0) Dashing(); else timer1 -= 1 * Time.deltaTime;
    }

    public void Dashing()
    {
        anim.ChangeAnimation("Dash");
        this.transform.position += this.transform.right * 14 * Time.deltaTime;
        timer2 -= 1 * Time.deltaTime;
        if (timer2 > 1.5f) { this.transform.position += this.transform.up * 8 * Time.deltaTime;}
        if(timer2 < 1f) this.transform.position -= this.transform.up * 8 * Time.deltaTime;
    }
}
