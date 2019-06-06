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
            //rb.velocity = new Vector2(speedX, rb.velocity.y);
            rb.AddForce(Vector3.up * 5 + Vector3.right * Input.acceleration.x);
        }
    }

    private void OnBecameInvisible()
    {
        print("invisible");
        var worldToScreenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        
        if (worldToScreenPoint.x > Screen.width || worldToScreenPoint.x < 0)
        {
            transform.localPosition.Set(worldToScreenPoint.x < 0 ? Screen.width : 0, transform.localPosition.y, transform.localPosition.z);
        }
    }
}