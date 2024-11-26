using Mirror;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LimbInputHandler : NetworkBehaviour
{
    public LimbDataContainer container;
    public bool carryinputblock;
    public Vector2 RawMovementInput;
    public bool isshotblocked;
    public Limb limb;

    private void Awake()
    {
        container = GameManager.instance.Ldcontainer;
        limb = GetComponent<Limb>();
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
                limb.CmdSetNormInputX((int)(RawMovementInput * Vector2.right).normalized.x);
                limb.CmdSetNormInputY((int)(RawMovementInput * Vector2.right).normalized.y);
            }
        }
    }

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

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            if (context.started && !isshotblocked)
            {
                container.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                container.mousePosition.z = 0;
                if (isServer)
                {
                    container.attackInput = true;
                }
                else
                {
                    limb.CmdSetattackInput(true);
                }
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
