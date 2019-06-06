using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform starContainer;

    public int numberOfStars = 500;

    public float levelWidth = 2f;
    public float minY = .01f;
    public float maxY = .1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0,0,1);

        for (int i = 0; i < numberOfStars; i++)
        {
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            spawnPosition.y += Random.Range(minY, maxY);
            var star = Instantiate(starPrefab, spawnPosition, Quaternion.identity, starContainer);

            var scale = Random.Range(-0.2f, 0f);
            
            star.transform.localScale = new Vector3(scale,scale,scale);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}