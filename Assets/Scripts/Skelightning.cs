using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelightning : MonoBehaviour
{
    public bool goRight;
	
	// Update is called once per frame
	void Update ()
    {
        if (goRight) this.transform.position -= this.transform.up * 15 * Time.deltaTime; else this.transform.position += this.transform.up * 15 * Time.deltaTime;
        Destroy(this.gameObject, 4);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.layer == 8 || c.gameObject.layer == 9 || c.gameObject.layer ==  0)
        {
            Destroy(this.gameObject);
        }
    }
}
