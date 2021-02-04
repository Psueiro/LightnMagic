using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFollowCustom : MonoBehaviour
{

    public GameObject target;
    public Camera cam;

    public bool allowSmooth;
    public Vector2 smooth;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;


        if (allowSmooth) FollowTargetSmooth();
        else FollowTarget();

    }

    public void FollowTargetSmooth()
    {
        Vector3 newPosition;

        newPosition.x = ((target.transform.position.x - cam.transform.position.x) / smooth.x) * Time.deltaTime;
        //newPosition.y = ((target.transform.position.y - cam.transform.position.y) / smooth.y) * Time.deltaTime;
        newPosition.y = 0;
        newPosition.z = 0;

        cam.transform.position += newPosition;

    }

    public void FollowTarget()
    {
        Vector3 newPosition;

        newPosition.x = target.transform.position.x;
        newPosition.y = cam.transform.position.y;

        newPosition.z = cam.transform.position.z;

        cam.transform.position = newPosition;

    }
}
