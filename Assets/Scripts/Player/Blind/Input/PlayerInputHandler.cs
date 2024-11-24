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
    public Player player;

    private void Awake()
    {
        container = GameManager.instance.Pdcontainer;
        player = GetComponent<Player>();
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
                player.CmdSetNormInputX((int)(RawMovementInput * Vector2.right).normalized.x);
                player.CmdSetNormInputY((int)(RawMovementInput * Vector2.right).normalized.y);
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
                    player.CmdSetLadderUp(true);
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
                    player.CmdSetLadderUp(false);
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
                        player.CmdSetLadderDown(true);
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
                        player.CmdSetLadderDown(false);
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
                        player.CmdSetSitInput(true);
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
                        player.CmdSetSitInput(false);
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
                    player.CmdSetJumpInput(true);
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
                        if (isServer)
                        {
                            container.throwinputtime = Time.time - throwinputtime;
                            throwinputtime = 0;
                            container.throwcall = true;
                        }
                        else
                        {
                            player.CmdSetThrowInputTime(Time.time - throwinputtime);
                            throwinputtime = 0;
                            player.CmdSetThrowCall(true);
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
                            player.CmdSetCarryUpCall(true);
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
                            player.CmdSetCarryUpCall(false);
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
