using UnityEngine;
using System.Collections;

public class Flakes : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;

    public GameObject target;
    public GameObject Flake;

    public bool MoveLeft;
    public bool MoveRight;
    public bool MoveUp;


    public float num;
    public float num2;



    // Use this for initialization
    void Start ()
    {
        num = Random.Range(0, 10);
        num2 = Random.Range(0, 10);

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

        if (num2 > 5)
        {
            MoveUp = true;
        }
        else MoveUp = false;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (MoveLeft)
        {
            MoveRight = false;
            Flake.transform.position -= Flake.transform.right * 6 * Time.deltaTime;

        }
        else if (MoveRight)
        {
            MoveLeft = false;
            Flake.transform.position += Flake.transform.right * 6 * Time.deltaTime;

        }

        if (MoveUp == true)
        {
            Flake.transform.position += Flake.transform.up * num * Time.deltaTime;
        }else Flake.transform.position -= Flake.transform.up * num * Time.deltaTime;


        if (Flake.transform.position.y > 40)
        {
            Destroy(this.gameObject);
        }else if (Flake.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0 || c.gameObject.layer == 8 || c.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
        }       
    }
}
