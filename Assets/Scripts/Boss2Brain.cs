using UnityEngine;
using System.Collections;

public class Boss2Brain : MonoBehaviour
{

    public GameObject target;
    public Boss2 body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Boss2>();
        target = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        if (body.Idle == true && target.transform.position.x > transform.position.x)
        {
            body.transform.localScale = new Vector3(-6, 6, 0);
            body.LookLeft = false;
        }else if (body.Idle == true && target.transform.position.x < transform.position.x)
        {
            body.transform.localScale = new Vector3(6, 6, 0);
            body.LookLeft = true;
        }

        //Anti Air
        if (body.LookLeft == true && transform.position.y < target.transform.position.y && body.Idle == true && transform.position.x <= target.transform.position.x + 6 /* && transform.position.x > target.transform.position.x */)
        {
            body.SlashingUp();
        }else if (body.LookLeft == false && transform.position.y < target.transform.position.y && body.Idle == true && transform.position.x >= target.transform.position.x - 6 /* && transform.position.x < target.transform.position.x*/)
        {
            body.SlashingUp();
        }
        else
        {
            body.ResetCooldowns();
        }

        //Idle Trigger
        if(body.IdleTime <= 0)
        {            
            if (Random.Range(0, 100) < 50)
            {
                 body.Dashing = true;

            }else body.Jump();
        }

        //Jump Trigger
        if (transform.position.y > target.transform.position.y && body.JumpFrames <= 0 && body.Jumping == true)
        {
            if (Random.Range(0, 100) < 50)
            {
                 body.Dashing = true;

            } else body.SlashingDown();
        }

        //Dash Stopper
        if (body.LookLeft == true && transform.position.x <= target.transform.position.x - 6)
        {
            body.Dashing = false;
            body.CanGetDamaged = true;
            body.Idle = true;
        }
        else if (body.LookLeft == false && transform.position.x >= target.transform.position.x + 6)
        {
            body.Dashing = false;
            body.CanGetDamaged = true;
            body.Idle = true;           
        }
    }
        
}
