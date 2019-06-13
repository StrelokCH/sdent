using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGenerator : MonoBehaviour
{
    private Vector3 _startPosition;
    private CameraFollow _cameraFollow;
    private Vector3 _starSpawnPosition = new Vector3(0, 0, 1);
    private float minY = 0.1f;
    private float maxY = 0.5f;

    public GameObject starPrefab;
    public int numberOfStars = 200;
    public Transform starContainer;

    public GameObject asteroidPrefab;
    public Transform asteroidContainer;
    private bool _spawnAsteroids;

    public GameObject marsPrefab;

    public GameObject rocket;
    private RocketNavigator _rocketNavigator;
    public GameObject restartContainer;
    public GameObject TurnToLandContainer;
    public GameObject CongratulationsContainer;
    public GameObject FuckedUpContainer;
    readonly float saveZone = 10f;

    private bool isOver;

    public static float marsHeight = 80f;
    public static float landingHeight = 60f;
    public static float levelWidth = 2f;
    public static float levelHeight = 8f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraFollow = Camera.main.gameObject.AddComponent<CameraFollow>();
        _cameraFollow.target = rocket.transform;
        _rocketNavigator = rocket.GetComponent<RocketNavigator>();
        restartContainer.SetActive(false);
        TurnToLandContainer.SetActive(false);
        CongratulationsContainer.SetActive(false);
        FuckedUpContainer.SetActive(false);

        Instantiate(marsPrefab, new Vector3(0, marsHeight, 0), Quaternion.identity);

        SpawnStars();
        InvokeRepeating("CreateAsteroid", 5f, 1.5f);
    }

    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0));
        Debug.Log(position.y);

        if (!_spawnAsteroids && !isOver)
        {
            if (position.y > saveZone)
            {
                _spawnAsteroids = true;
            }
        }

        if (rocket.transform.position.y > _starSpawnPosition.y - 20)
        {
            SpawnStars();
        }

        if (rocket.transform.position.y < position.y - levelHeight / 2)
        {
            OnRocketDied();
        }

        if (rocket.transform.position.y > landingHeight && !isOver)
        {
            isOver = true;
            _spawnAsteroids = false;
            TurnToLandContainer.SetActive(true);
            _rocketNavigator.ShowTurnToLand();
        }
    }

    public void OnRocketDied()
    {
        Debug.Log("You Died");
        restartContainer.SetActive(true);
        TurnToLandContainer.SetActive(false);
    }

    private void SpawnStars()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            _starSpawnPosition.y += Random.Range(minY, maxY);
            _starSpawnPosition.x = Random.Range(-levelWidth, levelWidth);
            var star = Instantiate(starPrefab, _starSpawnPosition, Quaternion.identity, starContainer);
            var scale = Random.Range(-0.2f, 0f);
            star.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    void CreateAsteroid()
    {
        if (_spawnAsteroids)
        {
            Instantiate(asteroidPrefab, asteroidContainer);
        }
    }

    public void Restart()
    {
        foreach (Transform child in starContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in asteroidContainer.transform)
        {
            Destroy(child.gameObject);
        }

        _starSpawnPosition = new Vector3(0, 0, 1);
        _cameraFollow.Reset();
        restartContainer.SetActive(false);
        TurnToLandContainer.SetActive(false);
        _spawnAsteroids = false;
        _rocketNavigator.Reset();
        CongratulationsContainer.SetActive(false);
        FuckedUpContainer.SetActive(false);
        isOver = false;
    }
}