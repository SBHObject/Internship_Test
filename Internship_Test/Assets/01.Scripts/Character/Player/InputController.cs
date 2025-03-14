using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInputSystem input;

    public event Action<Vector2> OnMoveInput;

    public event Action OnInteracteInput;
    public event Action OnEscapeInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        input = new PlayerInputSystem();

        input.Player.Move.performed += MoveInput;
        input.Player.Interaction.performed += InteracteInput;
        input.Player.Escape.performed += EscapeInput;
        input.Player.Enable();
    }

    private void MoveInput(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>().normalized);
    }

    private void InteracteInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteracteInput?.Invoke();
        }
    }

    private void EscapeInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnEscapeInput?.Invoke();
        }
    }
}
