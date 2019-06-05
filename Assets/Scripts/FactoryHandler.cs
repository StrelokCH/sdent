using UnityEngine;
using UnityEngine.UI;

public class FactoryHandler : MonoBehaviour
{
    private int step = 1;

    public GameObject[] Bodies;
    public GameObject[] Flames;
    public GameObject[] Wings;
    
    // Start is called before the first frame update
    void Start()
    {
        ShowSelected();
    }

    public void OnLeftClick()
    {
        print("Left");
        Change(-1);
        ShowSelected();
    }

    private void Change(int signum)
    {
        switch (step)
        {
            case 1:
                GameHandler.Instance.Rocket.Body = (GameHandler.Instance.Rocket.Body + signum) % 3;
                break;
            case 2:
                GameHandler.Instance.Rocket.Wings = (GameHandler.Instance.Rocket.Wings + signum) % 3;
                break;
            case 3:
                GameHandler.Instance.Rocket.Flame = (GameHandler.Instance.Rocket.Flame + signum) % 3;
                break;
        }
    }

    public void OnRightClick()
    {
        print("Right");
        Change(1);
        ShowSelected();
    }

    public void StepUp()
    {
        if (step < 3)
        {
            step++;
            GameObject.Find("Subtitle").GetComponent<Image>().sprite = Resources.Load<Sprite>("subtitle_" + step);
        }
        else
        {
            gameObject.GetComponent<PrefabLoader>().LoadPrefab();
        }
    }


    private void ShowSelected()
    {
        for (int i = 0; i < Bodies.Length; i++)
        {
            Bodies[i].SetActive(i == GameHandler.Instance.Rocket.Body);
        }

        for (int i = 0; i < Wings.Length; i++)
        {
            Wings[i].SetActive(i == GameHandler.Instance.Rocket.Wings);
        }

        for (int i = 0; i < Flames.Length; i++)
        {
            Flames[i].SetActive(i == GameHandler.Instance.Rocket.Flame);
        }
    }
}