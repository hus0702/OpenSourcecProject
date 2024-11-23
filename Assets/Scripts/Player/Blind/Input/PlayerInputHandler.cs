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

    public PlayerDataContainer container;

    private void Awake()
    {
        container = this.gameObject.GetComponent<PlayerDataContainer>();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            container.NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            container.NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
        }
    }

    public void ladderUpInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (context.performed)
            {
                container.ladderUp = true;
            }
            if (context.canceled)
            {
                container.ladderUp = false;
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
                    container.ladderDown = true;
                }
                if (context.canceled)
                {
                    container.ladderDown = false;
                }
            }
            else
            {
                if (context.performed)
                {
                    container.SitInput = true;
                }
                if (context.canceled)
                {
                    container.SitInput = false;
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
                container.JumpInput = true;
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
                        container.throwcall = true;
                    }
                }
                else
                {
                    if (context.started)
                    {
                        container.carryupcall = true;
                    }
                    if (context.canceled)
                    {
                        container.carryupcall = false;
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
