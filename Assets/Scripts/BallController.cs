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
        _rigidbody.AddForce(Random.insideUnitCircle.normalized * initialForce);
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
