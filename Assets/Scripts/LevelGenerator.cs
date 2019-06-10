using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject asteroidPrefab;
    public Transform starContainer;
    public GameObject rocket;

    public int numberOfStars = 500;

    public static float levelWidth = 2f;
    public static float levelHeight = 8f;
    public float minY = .01f;
    public float maxY = .1f;

    bool flying = false;
    readonly float flyingHeight = 20f;

    // Start is called before the first frame update
    void Start()
    {
        var cameraFollow = Camera.main.gameObject.AddComponent<CameraFollow>();
        cameraFollow.target = rocket.transform;
        
        Vector3 spawnPosition = new Vector3(0, 0, 1);

        for (int i = 0; i < numberOfStars; i++)
        {
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            spawnPosition.y += Random.Range(minY, maxY);
            var star = Instantiate(starPrefab, spawnPosition, Quaternion.identity, starContainer);

            var scale = Random.Range(-0.2f, 0f);

            star.transform.localScale = new Vector3(scale, scale, scale);
        }

        InvokeRepeating("CreateAsteroid", 5f, 1.5f);
    }

    void CreateAsteroid()
    {
        if (flying)
        {
            Instantiate(asteroidPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!flying)
        {
            var position =
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0));
            if (position.y > flyingHeight)
            {
                flying = true;
            }
        }
    }
}