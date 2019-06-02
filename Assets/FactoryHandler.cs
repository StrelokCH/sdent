using UnityEngine;
using UnityEngine.UI;

public class FactoryHandler : MonoBehaviour
{
    public int SelectedBody = 1;
    public int SelectedWings = 1;
    public int SelectedFlame = 1;

    private int step = 1;

    public GameObject[] Bodies;
    public GameObject[] Flames;
    public GameObject[] Wings;

    public static string PREFS_BODY = "Prefs_Body";
    public static string PREFS_WINGS = "Prefs_Wings";
    public static string PREFS_FLAMES = "Prefs_Flames";

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
                SelectedBody = (SelectedBody + signum) % 3;
                break;
            case 2:
                SelectedWings = (SelectedWings + signum) % 3;
                break;
            case 3:
                SelectedFlame = (SelectedFlame + signum) % 3;
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
            PlayerPrefs.SetInt(PREFS_BODY, SelectedBody);
            PlayerPrefs.SetInt(PREFS_WINGS, SelectedWings);
            PlayerPrefs.SetInt(PREFS_FLAMES, SelectedFlame);
            gameObject.GetComponent<PrefabLoader>().LoadPrefab();
        }
    }


    private void ShowSelected()
    {
        for (int i = 0; i < Bodies.Length; i++)
        {
            Bodies[i].SetActive(i == SelectedBody);
        }

        for (int i = 0; i < Wings.Length; i++)
        {
            Wings[i].SetActive(i == SelectedWings);
        }

        for (int i = 0; i < Flames.Length; i++)
        {
            Flames[i].SetActive(i == SelectedFlame);
        }
    }
}