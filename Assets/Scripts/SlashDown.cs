using UnityEngine;
using System.Collections;

public class SlashDown : MonoBehaviour
{
    public GameObject Slash2;
    public Boss2 un;
    public float MoveSpeed = 5;

    void Start()
    {
        un = GameObject.Find("Undyne(Clone)").GetComponent<Boss2>();
        if (un.LookLeft == false)
        {
            Slash2.transform.localScale = new Vector3(-5, 5, 0);
        }
    }

    void Update()
    {
        if (un.LookLeft == true)
        {
            Slash2.transform.position -= Slash2.transform.right * MoveSpeed * 1.5f * Time.deltaTime;
            Slash2.transform.position -= Slash2.transform.up * MoveSpeed * Time.deltaTime;
        }
        else if (un.LookLeft == false)
        {
            Slash2.transform.position += Slash2.transform.right * 1.5f * Time.deltaTime;
            Slash2.transform.position -= Slash2.transform.up * MoveSpeed * Time.deltaTime;
        }

   }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {
            Destroy(this.gameObject);
        }
        if (c.gameObject.layer == 4)
        {
            Destroy(this.gameObject);
        }
    }
}
