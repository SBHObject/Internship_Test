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
    public event Action OnNumber1Input;
    public event Action OnNumber2Input;

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

    public void Number1Input(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnNumber1Input?.Invoke();
        }
    }

    public void Number2Input(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnNumber2Input?.Invoke();
        }
    }    
}
