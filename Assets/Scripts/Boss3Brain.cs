using UnityEngine;
using System.Collections;

public class Boss3Brain : MonoBehaviour {

    public GameObject target;
    public Boss3 body;
    public int num;
    public bool numChosen;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Boss3>();
        target = GameObject.Find("Hero");
        numChosen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (body.Idle == true && target.transform.position.x > transform.position.x)
        {
            body.transform.localScale = new Vector3(-6, 6, 0);
            body.LookLeft = false;
        }
        else if (body.Idle == true && target.transform.position.x < transform.position.x)
        {
            body.transform.localScale = new Vector3(6, 6, 0);
            body.LookLeft = true;
        }

        if (body.IdleTime >= 100)
        {
            numChosen = false;
        }

        if (numChosen == false)
        {
            num = Random.Range(0, 100);
            numChosen = true;
        }

        if (body.IdleTime <= 0)
        {
            body.Idle = false;
            if (num < 25)
            {
                body.Dashing = true;

            }
            else if (num > 25 && num < 50)
            {
                body.CastingFire = true;
            }
            else if (num > 50 && num < 75)
            {
                body.CastingIce = true;
            }
            else body.CastingLightning = true;

        }

        if (body.LookLeft == true && transform.position.x <= target.transform.position.x - 6)
        {
            body.Dashing = false;
           // body.CanGetDamaged = true;
            body.Idle = true;
        }
        else if (body.LookLeft == false && transform.position.x >= target.transform.position.x + 6)
        {
            body.Dashing = false;
         //   body.CanGetDamaged = true;
            body.Idle = true;            
        }

    }
}
