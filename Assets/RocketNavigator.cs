using UnityEngine;

public class RocketNavigator : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject flame;

    float speedX;
    public float speed = 2f;

    void Update()
    {
        flame.SetActive(rb.velocity.y > 0);
        
        if (Input.touchCount > 0)
        {
            speedX = Input.acceleration.x * speed;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2f, 2f), transform.position.y);
            rb.velocity = new Vector2(speedX, 0f);
            rb.AddForce(Vector3.up * 5);
        }
    }

    private void OnBecameInvisible()
    {
        var worldToScreenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        
        if (worldToScreenPoint.x > Screen.width || worldToScreenPoint.x < 0)
        {
            transform.localPosition.Set(worldToScreenPoint.x < 0 ? Screen.width : 0, transform.localPosition.y, transform.localPosition.z);
        }
    }
}