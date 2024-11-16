using Mirror;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LimbInputHandler : NetworkBehaviour
{

    [SyncVar] public bool carryinputblock;
    [SyncVar] public Vector2 RawMovementInput;
    [SyncVar] public int NormInputX;
    [SyncVar] public int NormInputY;
    [SyncVar] public bool JumpInput;
    [SyncVar] public bool SitInput ;
    [SyncVar] public bool attackInput;
    [SyncVar] public Vector3 mousePosition;
    [SyncVar] bool isshotblocked;
    NetworkIdentity parentIdentity;

    private void Update()
    {
        if (parentIdentity == null)
        {
            parentIdentity = transform.parent.GetComponent<NetworkIdentity>();
        }
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (parentIdentity != null && parentIdentity.isOwned)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
        }
    }

    public void OnEscInput(InputAction.CallbackContext context)
    {
        if (parentIdentity != null && parentIdentity.isOwned) 
        { 
        
        }
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (parentIdentity != null && parentIdentity.isOwned)
        {

        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (parentIdentity != null && parentIdentity.isOwned)
        {
            if (context.started && !isshotblocked)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                attackInput = true;
            }
            if (context.canceled)
            {
                attackInput = false;
            }
        }
        
    }
    public IEnumerator stopcarryinput(float duration)
    {
        
        carryinputblock = true;
        yield return new WaitForSeconds(duration);

        carryinputblock = false;
        
        
    }

    public IEnumerator stopshotinput(float duration)
    {
        isshotblocked = true;
        yield return new WaitForSeconds(duration);

        isshotblocked = false;
    }
}
