using UnityEngine;
using UnityEngine.UI;

public class FactoryHandler : MonoBehaviour
{
    private int step = 1;

    public Text Title;
    public Text Description;

    public GameObject[] Bodies;
    public GameObject[] Flames;
    public GameObject[] Wings;


    // Start is called before the first frame update
    void Start()
    {
        ShowSelected();
        Change(0);
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
                GameHandler.Instance.Rocket.Body = GetNewValue(GameHandler.Instance.Rocket.Body, signum);
                Title.text = FactoryTexts.BodyTitle[GameHandler.Instance.Rocket.Body];
                Description.text = FactoryTexts.BodyDescription[GameHandler.Instance.Rocket.Body];
                break;
            case 2:
                GameHandler.Instance.Rocket.Wings = GetNewValue(GameHandler.Instance.Rocket.Wings, signum);
                Title.text = FactoryTexts.WingsTitle[GameHandler.Instance.Rocket.Wings];
                Description.text = FactoryTexts.WingsDescription[GameHandler.Instance.Rocket.Wings];
                break;
            case 3:
                GameHandler.Instance.Rocket.Flame = GetNewValue(GameHandler.Instance.Rocket.Flame, signum);
                Title.text = FactoryTexts.FlameTitle[GameHandler.Instance.Rocket.Flame];
                Description.text = FactoryTexts.FlameDescription[GameHandler.Instance.Rocket.Flame];
                break;
        }
    }

    private int GetNewValue(int value, int signum)
    {
        value = (value + signum) % 3;
        if (value < 0) value += 3;
        return value;
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
            Change(0);
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