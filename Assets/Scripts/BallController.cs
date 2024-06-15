using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public float initialForce;
    public float speedupModifier;

    private Rigidbody2D _rigidbody;

    private float timeTillLaunch = 4.0f;
    private bool launched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MoveInRandomDirection()
    {
        Vector2 randDirection = Random.insideUnitCircle.normalized;
        if (Vector2.Angle(randDirection, Vector2.up) <= 5 || Vector2.Angle(randDirection, Vector2.down) <= 5)
        {
            randDirection = Vector2.one.normalized;
        }
        _rigidbody.AddForce(randDirection * initialForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched)
        {
            timeTillLaunch -= Time.deltaTime;
            if (timeTillLaunch < 0)
            {
                MoveInRandomDirection();
                launched = true;
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_rigidbody.velocity.normalized * speedupModifier);
    }

    public void SetColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }

    public void Launch(Vector2 direction, float speed)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(speed * direction.normalized);
    }
}
