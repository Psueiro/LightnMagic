using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public GameObject LightningSpell;
    public float StayTime = 20;
    public float FallSpeed = 20;
    public bool Falling;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<AnimationControllerCustom>();
        rb = GetComponent<Rigidbody2D>();
        Falling = true;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Falling == true)
        {
            LightningSpell.transform.position += -LightningSpell.transform.up * FallSpeed * Time.deltaTime;
        }
        if (StayTime == 0)
        {
            Destroy(this.gameObject);
        }
        if (Falling == false)
        {
            StayTime--;
            anim.frame = 9;
        }
        if (LightningSpell.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {            
            Falling = false;
            SoundManager.instance.Stop(SoundID.LIGHTNING);
        }
        if (c.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
}
