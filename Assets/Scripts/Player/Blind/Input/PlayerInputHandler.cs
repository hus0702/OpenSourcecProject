using Mirror;
using Mirror.BouncyCastle.Asn1.BC;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : NetworkBehaviour
{
    public bool carryinputblock;
    public Vector2 RawMovementInput;
    public int NormInputX;
    public int NormInputY;
    public float throwinputtime;
    public bool JumpInput;
    public bool SitInput;
    public bool ladderUp;
    public bool ladderDown;


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            GameManager.instance.PlayerData.NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            GameManager.instance.PlayerData.NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
        }
    }

    public void ladderUpInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (context.performed)
            {
                GameManager.instance.PlayerData.ladderUp = true;
            }
            if (context.canceled)
            {
                GameManager.instance.PlayerData.ladderUp = false;
            }
        }
    }

    public void OnSitInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (GameManager.instance.PlayerData.isclimbing)
            {
                if (context.performed)
                {
                    GameManager.instance.PlayerData.ladderDown = true;
                }
                if (context.canceled)
                {
                    GameManager.instance.PlayerData.ladderDown = false;
                }
            }
            else
            {
                if (context.performed)
                {
                    GameManager.instance.PlayerData.SitInput = true;
                }
                if (context.canceled)
                {
                    GameManager.instance.PlayerData.SitInput = false;
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
                GameManager.instance.PlayerData.JumpInput = true;
            }
        }
           
    }
    public void UseJumpInput() => GameManager.instance.PlayerData.JumpInput = false;
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
