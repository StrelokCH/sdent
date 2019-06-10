using UnityEngine;
using UnityEngine.UI;

public class RocketLoader : MonoBehaviour
{
    public GameObject Body;
    public GameObject Wings;
    public GameObject Flame;

    // Start is called before the first frame update
    void Start()
    {
        print(GameHandler.Instance.Rocket.ToString());
        Body.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("body/body" + GameHandler.Instance.Rocket.Body);
        Wings.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("wings/wings" + GameHandler.Instance.Rocket.Wings);
        Flame.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("flames/flame" + GameHandler.Instance.Rocket.Flame);
    }
}