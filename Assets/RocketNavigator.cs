using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnMouseDown()
    {
        speedX = Input.acceleration.x * speed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2f, 2f), transform.position.y);

        rb.velocity = new Vector2(speedX, 0f);

        rb.AddForce(Vector3.up * 100);
    }

}