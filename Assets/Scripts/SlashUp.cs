using UnityEngine;
using System.Collections;

public class SlashUp : MonoBehaviour {

    public GameObject Slash1;
    public Boss2 un;
    public float MoveSpeed = 5;

    void Start()
    {
        un = GameObject.Find("Undyne(Clone)").GetComponent<Boss2>();
        if (un.LookLeft == false)
        {
            Slash1.transform.localScale = new Vector3(-5, 5, 0);
        }
    }

    void Update()
    {   
        if(un.LookLeft == true )
        {
            Slash1.transform.position -= Slash1.transform.right * MoveSpeed * Time.deltaTime;
            Slash1.transform.position += Slash1.transform.up * MoveSpeed * Time.deltaTime;
        }
        else if (un.LookLeft == false)
        {
            Slash1.transform.position += Slash1.transform.right * MoveSpeed * Time.deltaTime;
            Slash1.transform.position += Slash1.transform.up * MoveSpeed * Time.deltaTime;
        }

        if(Slash1.transform.position.y > 40)
        {
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
