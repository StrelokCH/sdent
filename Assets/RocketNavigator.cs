﻿using UnityEngine;
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

    //Set to true for auto take off :)
    private bool cheatMode = false;
    private Vector3 _startPosition;

    void Start()
    {
       _startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (_died) return;

        flame.SetActive(Input.touchCount > 0);
        if (Input.touchCount > 0 || cheatMode)
        {
            // Max. Upwards Velocity
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 9f);

            // Add Forces
            _inputUp = Vector3.up * 15;
            rb.AddRelativeForce(15 * Input.acceleration.x * Vector3.right);
            rb.AddRelativeForce(_inputUp + transform.rotation.eulerAngles * 100);

            // Add Rotation to the rocket object
            transform.rotation = Quaternion.Euler(0, 0, -Input.acceleration.x * 50 * 2);

            // For testing the values of Input.acceleration
            //text.text = Input.acceleration.x.ToString();
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

    public void ShowTurnToLand()
    {
        rb.isKinematic = true;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _died = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.angularDrag = 0f;
        gameObject.transform.rotation = Quaternion.identity;
        transform.localPosition = _startPosition;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}