using UnityEngine;
using System.Collections;

public class BossBrain : MonoBehaviour
{
    public GameObject target;
    public Boss body;

    // Use this for initialization
    void Start ()
    {
        body = GetComponent<Boss>();
        target = GameObject.Find("Hero");

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x < target.transform.position.x)
        {
            body.transform.localScale = new Vector3(-8, 8, 0);
            body.LookLeft = false;
        }else
        {
            body.transform.localScale = new Vector3(8, 8, 0);
            body.LookLeft = true;
        } 
    }
}
