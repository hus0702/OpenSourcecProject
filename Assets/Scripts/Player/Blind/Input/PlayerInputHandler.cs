using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool carryinputblock;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    float throwinputtime;
    public bool JumpInput { get; private set; }

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
        if (!carryinputblock)
        {
            if (GameManager.instance.PlayerData.iscarrying)
            {
                if (context.started)
                {
                    throwinputtime = Time.time;
                }
                else if (context.canceled)
                {
                    if ((Time.time - throwinputtime) > 0.5f)
                    {
                        Debug.Log("throwupcall");
                        GameManager.instance.PlayerData.throwcall = true;
                    }
                    else
                    {
                        Debug.Log("putdowncall");
                        GameManager.instance.PlayerData.putdowncall = true;
                    }
                }
            }
            else
            {
                if (context.started)
                {
                    GameManager.instance.PlayerData.carryupcall = true;
                }
                else if (context.canceled)
                {
                    GameManager.instance.PlayerData.carryupcall = false;
                }
            }
        }
        
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {

    }
    public IEnumerator stopcarryinput(float duration)
    {
        carryinputblock = true;
        yield return new WaitForSeconds(duration);

        carryinputblock = false;
    }
}
