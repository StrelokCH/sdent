using UnityEngine;
using UnityEngine.UI;

public class RocketNavigator : MonoBehaviour
{
    private float _speedX;
    private bool _rotating = true;
    private Vector3 _inputUp;
    private bool _died;

    public Rigidbody2D rb;
    public GameObject flame;
    public Text text;
    public LevelGenerator levelGenerator;
    private float forceStrength = 20f;
    private int rotSpeed = 40;

    void FixedUpdate()
    {
        if (_died) return;

        flame.SetActive(Input.touchCount > 0);
        if (Input.touchCount > 0)
        {
            Vector3 dir = rb.velocity;

            //forward or reverse thrust based on tilt
            _inputUp = Vector3.up;
            rb.AddRelativeForce(_inputUp * forceStrength);

            //rotation left or right based on tilt
            dir.z = Input.acceleration.x;

            //adding force to right or left based on tilt
            rb.AddRelativeForce(Vector3.forward * dir.z * forceStrength);

            // Limit up force
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 9f);

            // Rotate Rocket based on tilt of the phone
            if (dir.z > .2)
            {
                transform.Rotate(Vector3.forward * (-rotSpeed * Time.deltaTime), Space.World);
            }
            else if (dir.z < -.2)
            {
                transform.Rotate(Vector3.forward * (rotSpeed * Time.deltaTime), Space.World);
            }
        }
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Asteroid.AsteroidTag))
        {
            _died = true;
            levelGenerator.OnRocketDied();
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        gameObject.transform.rotation = Quaternion.identity;
        _died = false;
    }
}