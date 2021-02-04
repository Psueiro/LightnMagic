using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour
{

    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    //hacer que tome mage como referencia
    public Mage pm;
    public GameObject IceSpell;

    void Start()
    {
        anim = GetComponent<AnimationControllerCustom>();
        rb = GetComponent<Rigidbody2D>();
        pm = GameObject.Find("Hero").GetComponent<Mage>();
        SoundManager.instance.Play(SoundID.ICE);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
       if (pm.IceTime <= 0)
       {
            pm.IceAmount = 0;                       
            Destroy(this.gameObject);
            pm.CanIceAppear = false;
            SoundManager.instance.Stop(SoundID.ICE);
        }
        if (IceSpell.transform.position.y <= -20)
        {
            SoundManager.instance.Stop(SoundID.ICE);
            Destroy(this.gameObject);
            pm.IceAmount--;
            pm.IceTime = 180;
            pm.CanIceAppear = false;
        }

        Vector3 mousePositioInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePositioInWorld.y = IceSpell.transform.position.y;
        mousePositioInWorld.z = 0;
        IceSpell.transform.position = mousePositioInWorld;
    }
    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.layer == 12)
        {
            SoundManager.instance.Stop(SoundID.ICE);
            Destroy(this.gameObject);
            pm.IceAmount = 0;
            //  pm.IceTime = 0;
            pm.CanIceAppear = false;
        }
        if (c.gameObject.layer == 13)
        {
            SoundManager.instance.Stop(SoundID.ICE);
            Destroy(this.gameObject);
            pm.IceAmount = 0;
            // pm.IceTime = 0;
            pm.CanIceAppear = false;
        }
        if (c.gameObject.layer == 14)
        {
            SoundManager.instance.Stop(SoundID.ICE);
            Destroy(this.gameObject);
            pm.IceAmount = 0;
            // pm.IceTime = 0;
            pm.CanIceAppear = false;
        }
    }
}