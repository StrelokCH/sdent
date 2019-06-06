using System;
using System.Collections;
using System.Collections.Generic;
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
}