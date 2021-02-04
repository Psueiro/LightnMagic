using UnityEngine;
using System.Collections;

public class Boss3Spawner : MonoBehaviour {

    public GameObject Jamir;
    public bool EnemySpawned;


    void OnTriggerEnter2D(Collider2D c)
    {
        SpawnJamir();
    }

    void SpawnJamir()
    {
        if (EnemySpawned == false)
        {
            GameObject Boss3 = GameObject.Instantiate(Jamir);
            Boss3.transform.position = this.transform.position;
            EnemySpawned = true;
        }
    }
}
