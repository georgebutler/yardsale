using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform head;
    [SerializeField] private Transform feet;
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    
    private CharacterController _characterController;

    private float _headPitch;
    private Vector2 _movementInput;
    private Vector2 _lookInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
        
        transform.Rotate(Vector3.up * _lookInput.x);
        
        _headPitch -= _lookInput.y;
        _headPitch = Mathf.Clamp(_headPitch, -90f, 90f);
    }

    private void Update()
    {
        head.localRotation = Quaternion.Euler(_headPitch, 0, 0);
        
        var movement = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
        
        _characterController.Move(movement * (speed * Time.deltaTime));
    }
}