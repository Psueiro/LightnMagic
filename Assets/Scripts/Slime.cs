using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public bool IsFrozen;
    public bool IsKillable;
    public bool IsMoving;
    public bool Dying;
    public bool IsGrounded;
    public bool LookLeft;
    public float MoveSpeed = 5;
    public float FreezeTimer;
    public float DieFrames = 20;
    public float IdleTime = 20;
    public GameObject Slimey;

    // Use this for initialization
    void Start ()
    {
        IsFrozen = false;
        IsKillable = false;
        IsMoving = false;
        IsGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving && IsGrounded)
        {
            anim.ChangeAnimation("Move");
            if (LookLeft)
            {
                Slimey.transform.position -= Slimey.transform.right * MoveSpeed * Time.deltaTime;
            }else Slimey.transform.position += Slimey.transform.right * MoveSpeed * Time.deltaTime;

            if (anim.frame == 7)
            {
                IsMoving = false;                
            }
        }

        if (IsMoving == false && IsFrozen == false)
        {
            anim.ChangeAnimation("Idle");
            IdleTime--;
        }

        if(IdleTime == 0)
        {
            IsMoving = true;
            IdleTime = 20;
        }

        if (IsFrozen)
        {
            IsKillable = true;
            FreezeTimer--;
            if (FreezeTimer == 0)
            {
                IsMoving = true;
                IsFrozen = false;
                IsKillable = false;
            }
        }
        if (Slimey.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
        if (Dying)
        {
            DieFrames--;
            anim.ChangeAnimation("Die");
            IsMoving = false;
            if (DieFrames == 0)
            {
                Destroy(this.gameObject);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {
            IsMoving = true;
            IsGrounded = true;
        }
        if (c.gameObject.layer == 9)
        {
            if(IsFrozen)
            {
                if (Dying == false)
                {
                    IsMoving = true;
                    IsFrozen = false;
                    IsKillable = false;
                }
            }
        }
        if (c.gameObject.layer == 10)
        {
            anim.ChangeAnimation("Freeze");
            IsMoving = false;
            IsFrozen = true;
            IsKillable = true;
            FreezeTimer = 300;
        }

        if (c.gameObject.layer == 11)
        {
            if (IsKillable)
            {
                Dying = true;
                IsMoving = false;
                IsFrozen = false;
                IsKillable = false;
            }
            else if (MoveSpeed < 10)
            {
                MoveSpeed = MoveSpeed * 2;
                if (MoveSpeed > 10)
                {
                    MoveSpeed = 10;
                }
            }     
         }
        if (c.gameObject.layer == 15)
        {
            if (LookLeft)
            {
                Slimey.transform.localScale = new Vector3(-5, 5, 0);
                LookLeft = false;
            }
            else if (LookLeft == false)
            {
                Slimey.transform.localScale = new Vector3(5, 5, 0);
                LookLeft = true;
            }
        }

        if (c.gameObject.layer == 17)
        {
            if (LookLeft)
            {
                Slimey.transform.localScale = new Vector3(-5, 5, 0);
                LookLeft = false;
            }
            else if (LookLeft == false)
            {
                Slimey.transform.localScale = new Vector3(5, 5, 0);
                LookLeft = true;
            }
        }
        if (c.gameObject.layer == 8)
        {
            MoveSpeed = 0;
        }

    }
    void OnCollisionExit2D(Collision2D d)
    {
        if (d.gameObject.layer == 8)
        {
            MoveSpeed = 1;
        }
        if (d.gameObject.layer == 12)
        {
            MoveSpeed = 1;
        }
        if (d.gameObject.layer == 14)
        {
            MoveSpeed = 1;
        }
    }

}
