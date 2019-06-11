using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 startPosition;
    public Transform target;
    private float offsetFactor = 0.5f;

    private float TargetY => target.position.y + LevelGenerator.levelHeight * offsetFactor;

    // Update is called once per frame
    void Update()
    {
        if (TargetY > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, TargetY, transform.position.z);
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
