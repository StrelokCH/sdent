using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public static string AsteroidTag = "Asteroid";

    Vector2 velocity = new Vector2(0, -2);
    readonly float velocityRangeX = .5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        position.z = 0;

        var v = velocity + new Vector2
        (
            (Random.value - .5f) * LevelGenerator.levelWidth * velocityRangeX,
            0
        );
        GetComponent<Rigidbody2D>().velocity = v;
        float angle = Vector2.Angle(v, velocity);
        if (v.x < 0)
        {
            angle *= -1;
        }
        transform.Rotate(Vector3.forward * angle);

        transform.position = position + new Vector3
        (
            transform.position.x + (Random.value - .5f) * LevelGenerator.levelWidth,
            transform.position.y + LevelGenerator.levelHeight + Random.value * LevelGenerator.levelHeight,
            transform.position.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y < -Screen.width)
        {
            Destroy(gameObject);
        }
    }
}