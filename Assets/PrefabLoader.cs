using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    public GameObject Prefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadPrefab()
    {
        print("PRes");
        Instantiate(Prefab);
        Destroy(gameObject);
    }
}