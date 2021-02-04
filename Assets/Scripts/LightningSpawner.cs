using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{
    public GameObject jamir;
    public GameObject JamirLightning;
    public Boss3 un;
    public int num;
    public int num2;
    public int num3;

    // Use this for initialization
    void Start ()
    {
        num = 1;
        num2 = Random.Range(30, 60);
        num3 = num2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        jamir = GameObject.Find("Jamir(Clone)");
        if(jamir != null)
        {
            un = jamir.GetComponent<Boss3>();
            if (un.CastingLightning == true)
            {
                if (num > 0)
                {
                    GameObject Lightning = GameObject.Instantiate(JamirLightning);
                    Lightning.transform.position = this.transform.position;
                    num--;
                    num2 = num3;                
                }
                if (num <= 0)
                {
                    num2--;
                }
                if ( num2 <= 0)
                {
                    num = 1;
                }                      
            }
        }
    }


}
