using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using Mirror.BouncyCastle.Math.Field;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
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


    private void Update()
    {

        if (!isOwned)
            return;
        #region move
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isServer)
            {
                container.NormInputX = -1;
            }
            else
            {
                player.CmdSetNormInputX(-1);
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            {
                if (isServer)
                {
                    container.NormInputX = 0;
                }
                else
                {
                    player.CmdSetNormInputX(0);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isServer)
            {
                container.NormInputX = 1;
            }
            else
            {
                player.CmdSetNormInputX(1);
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (isServer)
            {
                container.NormInputX = 0;
            }
            else
            {
                player.CmdSetNormInputX(0);
            }
        }
        #endregion
        #region ladder & sit
        if (Input.GetKey(KeyCode.W))
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

        if (Input.GetKeyUp(KeyCode.W))
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

        if (Input.GetKey(KeyCode.S))
        {
            if (container.isclimbing)
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
            else
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
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            if (container.isclimbing)
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
            else
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

        #endregion
        #region jump
        if (Input.GetKeyDown(KeyCode.Space))
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
        #endregion
        #region Carry
        if (!carryinputblock)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (container.iscarrying)
                {
                    throwinputtime = Time.time;
                }
                else
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
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (container.iscarrying)
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
                else
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
        #endregion
        #region Interact
        if(Input.GetKeyDown(KeyCode.E))
        {

        }
        #endregion

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
