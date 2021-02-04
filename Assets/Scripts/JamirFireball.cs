using UnityEngine;
using System.Collections;

public class JamirFireball : MonoBehaviour
{
    public AnimationControllerCustom anim;
    public Rigidbody2D rb;
    public GameObject target;
    public GameObject Fireball;
    public GameObject Firepillar;
    public Boss3 un;

    // Use this for initialization
    void Start()
    {
        un = GameObject.Find("Jamir(Clone)").GetComponent<Boss3>();
        target = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(2, 2, 0);
        }
        else if (target.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 0);
        }

        if (un.LookLeft == true)
        {
            Fireball.transform.position -= Fireball.transform.right * 10 * Time.deltaTime;
            Fireball.transform.position -= Fireball.transform.up * 10 * Time.deltaTime;
        } else if (un.LookLeft == false)
        {
            Fireball.transform.position += Fireball.transform.right * 10 * Time.deltaTime;
            Fireball.transform.position -= Fireball.transform.up * 10 * Time.deltaTime;
        }

        if (Fireball.transform.position.y < -20)
        {
            Destroy(this.gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {
            Destroy(this.gameObject);
            GameObject FirePillar = GameObject.Instantiate(Firepillar);
            FirePillar.transform.position = this.transform.position;
        }
        if (c.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
    }
}
