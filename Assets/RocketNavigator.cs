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

    private Vector3 inputUp;

    public Text text;

    void Update()
    {

    }

    void FixedUpdate()
    {

        flame.SetActive(Input.touchCount > 0);
        if (Input.touchCount > 0)
        {
            // Max. Upwards Velocity
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 9f);

            // Add Forces
            inputUp  = Vector3.up * 15;
            rb.AddRelativeForce((15 * Input.acceleration.x * Vector3.right));
            rb.AddRelativeForce(inputUp + transform.rotation.eulerAngles * 100);

            // Add Rotation to the rocket object
            transform.rotation = Quaternion.Euler(0, 0, (-Input.acceleration.x * 50) * 2);

            // For testing the values of Input.acceleration
            text.text = Input.acceleration.x.ToString();
        }
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