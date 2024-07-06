using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{

    public Vector2 rotation;

    public UnityEvent onShootPerformed;
    public UnityEvent onCancelPerformed;
    public UnityEvent onRestartPerformed;

    public void OnRotation(InputAction.CallbackContext ctx)
    {
        rotation = ctx.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) onShootPerformed?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) onCancelPerformed?.Invoke();
    }

    public void OnRestart(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) onRestartPerformed?.Invoke();
    }

}
