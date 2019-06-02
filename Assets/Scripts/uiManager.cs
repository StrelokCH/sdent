using UnityEngine;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    public void Launch()
    {
        SceneManager.LoadScene("Launch");
    }
    public void Rocket()
    {
        SceneManager.LoadScene("Launch");
    }
    public void Config()
    {
        SceneManager.LoadScene("Launch");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
