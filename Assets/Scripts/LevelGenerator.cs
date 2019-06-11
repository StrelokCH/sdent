using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGenerator : MonoBehaviour
{
    private Vector3 _startPosition;
    private CameraFollow _cameraFollow;
    private bool _flying;
    private Vector3 _starSpawnPosition = new Vector3(0, 0, 1);
    private float minY = 0.1f;
    private float maxY = 0.5f;
    
    public GameObject starPrefab;
    public int numberOfStars = 200;
    public Transform starContainer;
    
    public GameObject asteroidPrefab;
    public Transform asteroidContainer;

    public GameObject rocket;
    public GameObject restartContainer;
    readonly float saveZone = 20f;
    
    public static float marsHeight = 200f;
    public static float levelWidth = 2f;
    public static float levelHeight = 8f;

    // Start is called before the first frame update
    void Start()
    {
        _cameraFollow = Camera.main.gameObject.AddComponent<CameraFollow>();
        _cameraFollow.target = rocket.transform;
        _startPosition = rocket.transform.position;
        restartContainer.SetActive(false);

        SpawnStars();
        InvokeRepeating("CreateAsteroid", 5f, 1.5f);
    }

    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, Screen.width / 2, 0));
        Debug.Log(position.y);

        if (!_flying)
        {
            if (position.y > saveZone)
            {
                _flying = true;
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
    }

    public void OnRocketDied()
    {
        Debug.Log("You Died");
        restartContainer.SetActive(true);
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
        if (_flying)
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
        rocket.transform.position = _startPosition;
        _cameraFollow.Reset();
        restartContainer.SetActive(false);
        _flying = false;
        rocket.GetComponent<RocketNavigator>().Reset();
    }
}