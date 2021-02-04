using UnityEngine;
using System.Collections;

public class Boss2Spawner : MonoBehaviour
{
    public GameObject Undyne;
    public bool EnemySpawned;

    void OnTriggerEnter2D(Collider2D c)
    {
        SpawnUndyne();
    }

    void SpawnUndyne()
    {
        if (EnemySpawned == false)
        {
            GameObject Boss2 = GameObject.Instantiate(Undyne);
            Boss2.transform.position = this.transform.position;
            EnemySpawned = true;
        }
    }
}
