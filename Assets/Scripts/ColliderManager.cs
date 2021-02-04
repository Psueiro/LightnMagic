using UnityEngine;
using System.Collections;

public class ColliderManager : MonoBehaviour {

    public Collider2D[] colliders;

    void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
    }

    public void ActiveCollider(string colliderName, bool value)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].name == colliderName) colliders[i].enabled = value;
        }
    }
}
