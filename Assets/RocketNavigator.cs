using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RocketNavigator : MonoBehaviour
{
    public String MarsTag = "Mars";
    private float _speedX;
    private bool _rotating = true;
    private Vector3 _inputUp;
    private bool _died;
    private bool _hickup;
    private bool _isLanding;

    public Rigidbody2D rb;
    public GameObject flame;
    public Text text;
    public LevelGenerator levelGenerator;

    private float MaxVelocity => 6f * GameHandler.Instance.Rocket.ThrustFactor;
    private float ForceStrength => 20f * GameHandler.Instance.Rocket.ThrustFactor;
    private float RotSpeed => 200 * GameHandler.Instance.Rocket.RotSpeedFactor;

    // hickup settings
    private float _hickupChance = 1f / 350f; // 50 checks per second
    private float _hickupDuration = 0.3f;

    // must be between 0 and 1
    // 1=no horizontal breaking
    // 0=horizontally almost fixed
    private float _horizontalBreak = 0.9f;

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

        // hicksup handling
        if (GameHandler.Instance.Rocket.HasHickups)
        {
            if (_hickup)
            {
                // flicker flame by fast disabling/enabling
                flame.SetActive(!flame.activeSelf);
                return;
            }
            else if (UnityEngine.Random.value < _hickupChance)
            {
                print("hickup!");
                _hickup = true;
                Invoke("EndHickup", _hickupDuration);
            }
        }

        flame.SetActive(Input.touchCount > 0);
        if (Input.touchCount > 0 || cheatMode)
        {
            Vector3 dir = rb.velocity;

            // decrease horizontal velocity to make handling of drift easier
            rb.velocity = new Vector2(rb.velocity.x * _horizontalBreak, rb.velocity.y);

            //forward or reverse thrust based on tilt
            _inputUp = Vector3.up;
            rb.AddRelativeForce(_inputUp * ForceStrength);

            //rotation left or right based on tilt
            dir.z = Input.acceleration.x;

            //adding force to right or left based on tilt
            rb.AddRelativeForce(Vector3.forward * dir.z);

            // Limit up force
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);

            // Rotate Rocket based on tilt of the phone
            transform.Rotate(Vector3.forward * (-RotSpeed * dir.z * Time.deltaTime), Space.World);
        }

        //text.text = Input.acceleration.y.ToString();
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
        else if (other.gameObject.CompareTag(MarsTag))
        {
            // Not working yet
            if (other.relativeVelocity.magnitude < 5f && _isLanding)
            {
                levelGenerator.CongratulationsContainer.SetActive(true);
            }
            else
            {
                _died = true;
                levelGenerator.OnRocketDied();
            }
        }
    }

    public void ShowTurnToLand()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        cheatMode = false;
        StartCoroutine(TurnAllUpside());
    }

    private IEnumerator TurnAllUpside()
    {
        yield return new WaitForSeconds(2);

        if (Input.acceleration.y > 0.7)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            rb.gravityScale = -1;
            _isLanding = true;
        }
        else
        {
            levelGenerator.FuckedUpContainer.SetActive(true);
            //"You missed it"
        }

        levelGenerator.TurnToLandContainer.SetActive(false);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void EndHickup()
    {
        print("end hickup");
        _hickup = false;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _died = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.angularDrag = 0f;
        rb.gravityScale = 1;
        gameObject.transform.rotation = Quaternion.identity;
        rb.mass = 1f * GameHandler.Instance.Rocket.MassFactor;
        transform.localPosition = _startPosition;
        rb.bodyType = RigidbodyType2D.Dynamic;
        _isLanding = false;
    }
}