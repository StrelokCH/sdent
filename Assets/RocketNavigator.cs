using UnityEngine;

public class RocketNavigator : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject flame;
    public Camera camera;
    float speedX;
    public float speed = 2f;

    void Update()
    {
        flame.SetActive(Input.touchCount > 0);
        if (Input.touchCount > 0)
        {
            rb.AddForce(Vector3.up * 15);
        }

        rb.AddForce(15 * Input.acceleration.x * Vector3.right);
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }
}