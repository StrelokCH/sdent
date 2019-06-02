using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameObject Body;
    public GameObject Wings;
    public GameObject Flame;
    // Start is called before the first frame update
    void Start()
    {
        LoadRocket();
    }

    private void LoadRocket()
    {
        Body.GetComponent<Image>().sprite = Resources.Load<Sprite>("body/body_" + PlayerPrefs.GetInt(FactoryHandler.PREFS_BODY));
        Wings.GetComponent<Image>().sprite = Resources.Load<Sprite>("wings/wings" + PlayerPrefs.GetInt(FactoryHandler.PREFS_WINGS));
        Flame.GetComponent<Image>().sprite = Resources.Load<Sprite>("flames/flame" + PlayerPrefs.GetInt(FactoryHandler.PREFS_FLAMES));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
