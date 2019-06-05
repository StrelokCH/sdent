using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Rocket Rocket = new Rocket();
    public GameObject StartPrefab;

    //Using Instance so that the Object is Singleton
    public static GameHandler Instance { get; private set; }

    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(StartPrefab);
    }

    // Update is called once per frame
    void Update()
    {
    }
}