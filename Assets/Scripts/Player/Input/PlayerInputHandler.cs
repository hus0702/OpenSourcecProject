using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool JumpInput { get; private set; }

    public bool CarryUpInput { get; private set; }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
    }


    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;
    public void OnEscInput(InputAction.CallbackContext context)
    {
        
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        
    }

    public void OnCarryUpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CarryUpInput = true;
        }
        else if (context.canceled)
        {
            CarryUpInput = false;
        }
        
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        
    }



}
