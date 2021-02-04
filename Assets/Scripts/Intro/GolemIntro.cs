using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIntro : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public BoxCollider2D boxc;

    public float timer1;

    // Use this for initialization
    void Start ()
    {
        anim = this.GetComponent<AnimationControllerCustom>();
        rb = this.GetComponent<Rigidbody2D>();
        boxc = this.GetComponent<BoxCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        timer1 = 1.1f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer1 <= 0) { Fall();} else timer1 -= 1 * Time.deltaTime;
        if (anim.currentAnimation.name == "Attack" && anim.frame == 6) Hop();
    }

    public void Fall()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void Hop()
    {
        anim.ChangeAnimation("Idle");
        rb.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
        boxc.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.layer == 0)
        {
            anim.ChangeAnimation("Attack");
        }
    }
}
