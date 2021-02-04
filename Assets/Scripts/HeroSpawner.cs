using UnityEngine;
using System.Collections;

public class HeroSpawner : MonoBehaviour
{
    public GameObject Hero;
    public Mage pm;

    // Use this for initialization
    void Start ()
    {
        pm = GameObject.Find("Hero").GetComponent<Mage>();
    }

    void Update()
    {
        if (pm.Dead == true)
        {
            SpawnHero();
            pm.Dead = false;
        }
    }

    void SpawnHero()
    {
        GameObject Hero1 = GameObject.Instantiate(Hero);
        Hero1.transform.position = this.transform.position;
    }

}
