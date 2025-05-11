using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    public static Vector2 MovementVector2; 
    private PlayerInput _playerInput;
    private InputAction _movementAction;
    public bool isInteracting;
    public  InputActionReference InteractAction => interactAction;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementAction = _playerInput.actions["Move"];
    }

    private void FixedUpdate()
    {
       isInteracting =  IsInteracting();
    }

    public bool IsInteracting()
    {
        return interactAction.action.IsPressed();
    }

    private void Update()
    {
        MovementVector2 = _movementAction.ReadValue<Vector2>();
    }
}
}


