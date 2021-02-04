using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public ColliderManager colliderManager;

    public bool Frozen;

    public float ThawCountdown = 600;

    // Use this for initialization
    void Start ()
    {
        Frozen = false;
        colliderManager = GetComponent<ColliderManager>();
        colliderManager.ActiveCollider("FrozenWater", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Frozen == true)
        {
            anim.ChangeAnimation("Frozen");
            ActivateHitCollider();
            ThawCountdown--;
        }
        if (ThawCountdown <= 0)
        {
            Frozen = false;
            ThawCountdown = 600;
        }

        if (Frozen == false)
        {
            anim.ChangeAnimation("Normal");
            DeactivateHitCollider();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == 10)
        {
            Frozen = true;
        }
        if (c.gameObject.layer == 9)
        {
             Frozen = false;           
            ThawCountdown = 600;
        }
        
    }
    void OnTriggerStay2D(Collider2D d)
    {
        if (d.gameObject.layer == 10)
        {
            Frozen = true;
        }
        if (d.gameObject.layer == 9)
        {
            Frozen = false;
            ThawCountdown = 600;
        }

        if (d.gameObject.layer == 19)
        {
            Frozen = false;
            ThawCountdown = 600;
        }
    }

    public void ActivateHitCollider()
    {
        colliderManager.ActiveCollider("FrozenWater", true);
        colliderManager.ActiveCollider("Water", false);
    }
    public void DeactivateHitCollider()
    {
        colliderManager.ActiveCollider("FrozenWater", false);
        colliderManager.ActiveCollider("Water", true);
    }
}
