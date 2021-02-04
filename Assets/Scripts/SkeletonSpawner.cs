using UnityEngine;
using System.Collections;

public class SkeletonSpawner : MonoBehaviour
{

    public GameObject Skelly;
    public bool EnemySpawned;


    void OnTriggerEnter2D(Collider2D c)
    {
        SpawnSkeleton();
    }

    void SpawnSkeleton()
    {       
        if (EnemySpawned == false)
        {
            GameObject Skeleton = GameObject.Instantiate(Skelly);
            Skeleton.transform.position = this.transform.position;
            EnemySpawned = true;
        }
    }

}
