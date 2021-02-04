using UnityEngine;
using System.Collections;

public class PlayerBrain : MonoBehaviour
{
    public Rigidbody2D rb;
    public Mage pm;
    public GameObject Hero;

    // Use this for initialization
    void Start ()
    {
       pm = this.GetComponent<Mage>();
       
    }
	
	// Update is called once per frame
	void Update ()
    {        
            if (Input.GetKeyDown(KeyCode.W) && pm.canJump)
        {
            rb.AddForce(Vector3.up * 7, ForceMode2D.Impulse);
            pm.Jumping = true;
            SoundManager.instance.Play(SoundID.JUMP);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pm.Ducking = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            pm.Ducking = false;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            pm.Casting = true;
            if (pm.IceTime == 0 && pm.IceAmount == 0)
            {
                pm.IceTime = 180;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
                pm.CanIceAppear = true;            
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            pm.Casting = false;
            if (pm.SpellMode == 2)
            {
            pm.IceTime = 0;                
            pm.IceAmount = 0;
            SoundManager.instance.Stop(SoundID.ICE);
            }
            pm.LightningAmount = 0;           
        }

        if (Input.GetKey(KeyCode.D))
        {
            pm.Running = true;
            if (pm.LookLeft)
            {
                Hero.transform.localScale = new Vector3(7, 7, 0);
                pm.LookLeft = false;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            pm.Running = true;
            if (pm.LookLeft == false)
            {
                Hero.transform.localScale = new Vector3(-7, 7, 0);
                pm.LookLeft = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            pm.Running = false;            
        }else if (Input.GetKeyUp(KeyCode.A))
        {
            pm.Running = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (pm.Casting == false)
                { 
            if(pm.SpellMode == 1)
            {
                pm.SpellMode = 3;
            }else if (pm.SpellMode == 2)
            {
                pm.SpellMode = 1;
            }else if (pm.SpellMode == 3)
            {
                pm.SpellMode = 2;
            }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pm.Casting == false)
            {
                if (pm.SpellMode == 1)
                {
                    pm.SpellMode = 2;
                }
                else if (pm.SpellMode == 2)
                {
                    pm.SpellMode = 3;
                }
                else if (pm.SpellMode == 3)
                {
                    pm.SpellMode = 1;
                }
            }
        }

    }
}
