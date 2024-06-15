using UnityEngine;

public class WallPieceForce : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public Vector2 startingForce;
    public float torque;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartExplosion();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -4.0f)
        {
            Destroy(gameObject);
        }
    }

    void StartExplosion()
    {
        _rigidbody.AddForce(startingForce);
        _rigidbody.AddTorque(torque);
    }
}
