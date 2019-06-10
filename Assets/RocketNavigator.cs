using UnityEngine;
using UnityEngine.UI;

public class RocketNavigator : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject flame;
    public Camera camera;
    float speedX;
    public float speed = 2f;

    private bool rotating = true;

    private Vector3 inputX = Vector3.zero;

    public Text text;
    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

        flame.SetActive(Input.touchCount > 0);
        if (Input.touchCount > 0)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 9f);

            inputX = Vector3.up * 15f;

            rb.AddRelativeForce((15 * Input.acceleration.x * Vector3.right));
            rb.AddRelativeForce(inputX + transform.rotation.eulerAngles * 100);

            transform.rotation = Quaternion.Euler(0, 0, (-Input.acceleration.x * 50) * 2);

            text.text = Input.acceleration.x.ToString();

            /*  if (rotating && Input.acceleration.x > 0)
             {
                 Vector3 to = new Vector3(0, 0, 45);
                 Debug.Log(Vector3.Distance(transform.eulerAngles, to));

                 if (Vector3.Distance(transform.eulerAngles, to) > 0.01f)
                 {
                     transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
                 }
                 else
                 {
                     transform.eulerAngles = to;
                     rotating = false;
                 }
             }*/
        }




        //if (transform.rotation.z >= 0 && transform.rotation.z < 2)
        //{
        //    transform.Rotate(Vector3.forward);
        //}
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Asteroid.AsteroidTag)
        {
            // Todo: add effect if rocket is hit

        }
    }
}