using UnityEngine;
using System.Collections;

public class GargoyleSpawner : MonoBehaviour
{
    public GameObject Goyle;
    public bool EnemySpawned;


    void OnTriggerEnter2D(Collider2D c)
    {
        SpawnGargoyle();
    }

    void SpawnGargoyle()
    {
        if (EnemySpawned == false)
        {
            GameObject Gargoyle = GameObject.Instantiate(Goyle);
            Gargoyle.transform.position = this.transform.position;
            EnemySpawned = true;
        }
    }
}
