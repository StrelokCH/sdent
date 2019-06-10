using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject starPrefab;
    public GameObject asteroidPrefab;
    public Transform starContainer;
    public Transform asteroidContainer;
    public GameObject rocket;

    public GameObject restartButton;

    public int numberOfStars = 200;

    public static float marsHeight = 200f;
    public static float levelWidth = 2f;
    public static float levelHeight = 8f;
    public float minY = .01f;
    public float maxY = .05f;

    bool flying;
    readonly float flyingHeight = 20f;
    Vector3 starSpawnPosition = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        var cameraFollow = Camera.main.gameObject.AddComponent<CameraFollow>();
        cameraFollow.target = rocket.transform;
        SpawnStars();


        InvokeRepeating("CreateAsteroid", 5f, 1.5f);
    }

    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0));
        Debug.Log(position.y);

        if (!flying)
        {
            if (position.y > flyingHeight)
            {
                flying = true;
            }
        }

        if (rocket.transform.position.y > starSpawnPosition.y - 20)
        {
            SpawnStars();
        }

        if (rocket.transform.position.y < position.y - levelHeight/2)
        {
            Debug .Log("You Died");
            restartButton.SetActive(true);
        }
    }

    private void SpawnStars()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            starSpawnPosition.y += Random.Range(minY, maxY);
            starSpawnPosition.x = Random.Range(-levelWidth, levelWidth);
            var star = Instantiate(starPrefab, starSpawnPosition, Quaternion.identity, starContainer);
            var scale = Random.Range(-0.2f, 0f);
            star.transform.localScale = new Vector3(scale, scale, scale);
        }

    }

    void CreateAsteroid()
    {
        if (flying)
        {
            Instantiate(asteroidPrefab, asteroidContainer);
        }
    }
}