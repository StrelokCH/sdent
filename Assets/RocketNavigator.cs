using UnityEngine;

public class RocketNavigator : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject flame;

    void Update()
    {
        flame.SetActive(rb.velocity.y > 0);
    }

    private void OnMouseDown()
    {
        rb.AddForce(Vector3.up * 100);
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