using System;
using UnityEngine;

public class CPUPaddleController : MonoBehaviour
{
    private float _rangeOfMotion = 3.65f;

    public float _moveModifier = 0.06f;

    private GameObject _ball;
    
    private Rigidbody2D _rigidbody2D;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ball = GameObject.Find("Ball");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (GameManager.i.State == GameState.PONGTransition)
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
            _rigidbody2D.excludeLayers = LayerMask.NameToLayer("Default");
        }
        else
        {
            // Debug.Log(_ball);
            if (_ball == null)
            {
                _ball = GameObject.Find("Ball(Clone)");
            }
            else
            {
                // Debug.Log(_ball.transform.position);
                if (_ball.transform.position.x > 0)
                {
                    float diff = _ball.transform.position.y - transform.position.y;
                    float newYOffset = Math.Clamp(diff, -_moveModifier, _moveModifier);
                    float newYPos = Math.Clamp(transform.position.y + newYOffset, -_rangeOfMotion, _rangeOfMotion);
                    transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
                }
            }
        }

        if (transform.position.y < -4)
        {
            Destroy(this);
        }
    }
}
