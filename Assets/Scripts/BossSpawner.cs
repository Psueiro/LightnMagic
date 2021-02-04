using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour
{
    public GameObject Golem;
    public bool EnemySpawned;

    void OnTriggerEnter2D(Collider2D c)
    {
        SpawnGolem();
    }

    void SpawnGolem()
    {
        if (EnemySpawned == false)
        {
            GameObject Boss = GameObject.Instantiate(Golem);
            Boss.transform.position = this.transform.position;
            EnemySpawned = true;
        }
    }
}
