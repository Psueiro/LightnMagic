using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbIntro : MonoBehaviour
{
    public GameObject target;
    public float dis;
    public bool shrink;
    public Vector3 tarPos;
    public SpriteRenderer spr;
    public float timer;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Hero");
        spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Limiting();
        timer += 1 * Time.deltaTime;
        switch(this.transform.gameObject.name)
        {
            case "ORB":
                Behavior1();
                break;
            case "ORB2":
                Behavior2();
                break;
            case "ORB3":
                Behavior3();
                break;
        }
    }

    public void Limiting()
    {
        if (dis > 1.5f) shrink = true;
        if (dis < -1.5f) shrink = false;

        if (shrink) { dis -= 1 * 3 * Time.deltaTime; spr.sortingOrder = 0;}
        else { dis += 1 * 3 * Time.deltaTime; spr.sortingOrder = 2; }

            tarPos = target.transform.position;
    }

    public void Behavior1()
    {
        this.transform.position = new Vector3(tarPos.x + dis, tarPos.y + dis);
        if (timer > 2.2f) target = GameObject.Find("Golem");
    }

    public void Behavior2()
    {
        this.transform.position = new Vector3(tarPos.x + dis * - 1, tarPos.y + dis);
        if (timer > 3.8f) target = GameObject.Find("Undyne");
    }

    public void Behavior3()
    {
        this.transform.position = new Vector3(tarPos.x + dis, tarPos.y);
        if (timer > 5) target = GameObject.Find("Jamir");
    }
}
