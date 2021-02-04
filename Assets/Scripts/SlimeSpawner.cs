using UnityEngine;
using System.Collections;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject Slimey;
    public bool EnemySpawned;

    void OnTriggerEnter2D(Collider2D c)
    {
        SpawnSlime();
    }

    void SpawnSlime()
    {
        if (EnemySpawned == false)
        {
            GameObject Skeleton = GameObject.Instantiate(Slimey);
            Skeleton.transform.position = this.transform.position;
            EnemySpawned = true;
        }

    }
	

}
