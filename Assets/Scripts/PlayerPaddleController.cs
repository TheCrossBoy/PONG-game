using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddleController : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _rotateAction;
    
    private Vector3 _lastMousePosition;

    public float moveModifier;
    public float rotationModifier;

    private float _rangeOfMotion = 3.65f;
    private float _rangeOfRotation = 30;
    
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Movement");
        _rotateAction = InputSystem.actions.FindAction("Rotation");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, Math.Clamp(_moveAction.ReadValue<float>() * moveModifier + transform.position.y, -_rangeOfMotion, _rangeOfMotion), transform.position.z);
        transform.Rotate(Vector3.forward, -_rotateAction.ReadValue<float>() * rotationModifier);
        if ((transform.eulerAngles.z < 180 && transform.eulerAngles.z > _rangeOfRotation) ||
            (transform.eulerAngles.z >= 180 && transform.eulerAngles.z < 360.0 - _rangeOfRotation)) 
        {
            // Debug.Log(transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(0, 0, _rangeOfRotation * (transform.eulerAngles.z < 180 ? 1 : -1));
        }
    }
}
