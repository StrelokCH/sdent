using UnityEngine;
using UnityEngine.UI;

public class RocketLoader : MonoBehaviour
{
    public GameObject Body;
    public GameObject Wings;
    public GameObject Flame;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        LoadRocket();
    }

    private void LoadRocket()
    {
        print(GameHandler.Instance.Rocket.ToString());
        Body.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("body/body" + GameHandler.Instance.Rocket.Body);
        Wings.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("wings/wings" + GameHandler.Instance.Rocket.Wings);
        Flame.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("flames/flame" + GameHandler.Instance.Rocket.Flame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 500);
            print("space key was pressed");
        }
    }
}