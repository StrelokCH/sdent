using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 startPosition;
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        if (target.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }

    void Start()
    {
        startPosition = transform.position;
    }

    public void Reset()
    {
        transform.position = startPosition;
    }
}
