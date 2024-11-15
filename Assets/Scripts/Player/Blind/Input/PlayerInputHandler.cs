using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : NetworkBehaviour
{
    [SyncVar] public bool carryinputblock;
    [SyncVar] public Vector2 RawMovementInput;
    [SyncVar] public int NormInputX;
    [SyncVar] public int NormInputY;
    [SyncVar] public float throwinputtime;
    [SyncVar] public bool JumpInput;
    [SyncVar] public bool SitInput;
    [SyncVar] public bool ladderUp;
    [SyncVar] public bool ladderDown;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
    }

    public void ladderUpInput(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            ladderUp = true;
        }
        if (context.canceled)
        {
            ladderUp = false;
        }
        
    }

    public void OnSitInput(InputAction.CallbackContext context)
    {
        if (GameManager.instance.PlayerData.isclimbing)
        {
            if (context.performed)
            {
                ladderDown = true;
            }
            if (context.canceled)
            {
                ladderDown = false;
            }
        }
        else
        {
            if (context.performed)
            {
                SitInput = true;
            }
            if (context.canceled)
            {
                SitInput = false;
            }
        }

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
                if (context.canceled)
                {
                    GameManager.instance.PlayerData.throwinputtime = Time.time - throwinputtime;
                    throwinputtime = 0;
                    GameManager.instance.PlayerData.throwcall = true;
                }
            }
            else
            {
                if (context.started)
                {
                    GameManager.instance.PlayerData.carryupcall = true;
                }
                if (context.canceled)
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
