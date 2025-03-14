using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    public event Action<Vector2> OnMoveInput;

    public event Action OnInteracteInput;
    public event Action OnEscapeInput;

    public void MoveInput(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>().normalized);
    }

    public void InteracteInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteracteInput?.Invoke();
        }
    }

    public void EscapeInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnEscapeInput?.Invoke();
        }
    }
}
