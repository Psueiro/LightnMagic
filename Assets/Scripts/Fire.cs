using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{

    public AnimationControllerCustom anim;
    public Rigidbody2D rb;


    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<AnimationControllerCustom>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
	
	// Update is called once per frame
	void Update ()
    {
      
        if (anim.frame == 5)
        {
            Destroy(this.gameObject);
            SoundManager.instance.Stop(SoundID.FIRE);
        }
	}
}
