using UnityEngine;
using System.Collections;

public class FirePillar : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;

    public GameObject Firetower;
    public GameObject target;

    public bool MoveLeft;
    public bool MoveRight;

    // Use this for initialization
    void Start ()
    {
        //un = GameObject.Find("Jamir").GetComponent<Boss3>();
        target = GameObject.Find("Hero");
        if (target.transform.position.x < transform.position.x)
        {
            MoveLeft = true;
            MoveRight = false;
        }
        else if (target.transform.position.x > transform.position.x)
        {
            MoveLeft = false;
            MoveRight = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        SoundManager.instance.Play(SoundID.FIRE);
        if (MoveLeft)
        {
            MoveRight = false;
            Firetower.transform.position -= Firetower.transform.right * 6 * Time.deltaTime;

        }   else if (MoveRight)
        {
            MoveLeft = false;
            Firetower.transform.position += Firetower.transform.right * 6 * Time.deltaTime;

        }
        if (Firetower.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
        if (c.gameObject.layer == 17)
        {
            Destroy(this.gameObject);
        }
    }
}
