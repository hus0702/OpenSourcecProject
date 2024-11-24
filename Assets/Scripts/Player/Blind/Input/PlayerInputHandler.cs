using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : NetworkBehaviour
{
    public bool carryinputblock;
    public Vector2 RawMovementInput;
    public float throwinputtime;

    public PlayerDataContainer container;

    private void Awake()
    {
        container = GameManager.instance.Pdcontainer;
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            if (isServer)
            {
                container.NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
                container.NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
            }
            else 
            {
                container.CmdSetNormInputX((int)(RawMovementInput * Vector2.right).normalized.x);
                container.CmdSetNormInputY((int)(RawMovementInput * Vector2.right).normalized.y);
            }
        }
    }

    public void ladderUpInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (context.performed)
            {
                if (isServer)
                {
                    container.ladderUp = true;
                }
                else
                {
                    container.CmdSetLadderUp(true);
                }
            }
            if (context.canceled)
            {
                if (isServer)
                {
                    container.ladderUp = false;
                }
                else
                {
                    container.CmdSetLadderUp(false);
                }
                
            }
        }
    }

    public void OnSitInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (container.isclimbing)
            {
                if (context.performed)
                {
                    if (isServer)
                    {
                        container.ladderDown = true;
                    }
                    else
                    {
                        container.CmdSetLadderDown(true);
                    }
                }
                if (context.canceled)
                {
                    if (isServer)
                    {
                        container.ladderDown = false;
                    }
                    else
                    {
                        container.CmdSetLadderDown(false);
                    }
                }
            }
            else
            {
                if (context.performed)
                {
                    if (isServer)
                    {
                        container.SitInput = true;
                    }
                    else
                    {
                        container.CmdSetSitInput(true);
                    }
                }
                if (context.canceled)
                {
                    if (isServer)
                    {
                        container.SitInput = false;
                    }
                    else
                    {
                        container.CmdSetSitInput(false);
                    }
                }
            }
        }
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (context.started)
            {
                if (isServer)
                {
                    container.JumpInput = true;
                }
                else
                { 
                    container.CmdSetJumpInput(true);
                }
            }
        }
           
    }
    public void UseJumpInput() => container.JumpInput = false;
    public void OnEscInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        { 
        
        }
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {

        }
    }

    public void OnCarryUpInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (!carryinputblock)
            {
                if (container.iscarrying)
                {
                    if (context.started)
                    {
                        throwinputtime = Time.time;
                    }
                    if (context.canceled)
                    {
                        container.throwinputtime = Time.time - throwinputtime;
                        throwinputtime = 0;
                        if (isServer)
                        {
                            container.throwcall = true;
                        }
                        else
                        {
                            container.CmdSetThrowCall(true);
                        }
                    }
                }
                else
                {
                    if (context.started)
                    {
                        if (isServer)
                        {
                            container.carryupcall = true;
                        }
                        else
                        { 
                            container.CmdSetCarryUpCall(true);
                        }
                    }
                    if (context.canceled)
                    {
                        if (isServer)
                        {
                            container.carryupcall = false;
                        }
                        else
                        {
                            container.CmdSetCarryUpCall(false);
                        }
                    }
                }
            }
        }
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
           
        }
    }
    public IEnumerator stopcarryinput(float duration)
    {
        carryinputblock = true;
        yield return new WaitForSeconds(duration);

        carryinputblock = false;
    }
}
