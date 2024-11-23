using Mirror;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LimbInputHandler : NetworkBehaviour
{

    public bool carryinputblock;
    public Vector2 RawMovementInput;
    public int NormInputX;
    public int NormInputY;
    public bool JumpInput;
    public bool SitInput;
    public bool attackInput;
    public Vector3 mousePosition;
    public bool isshotblocked;

    private void Update()
    {
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (isOwned)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            GameManager.instance.LimbData.NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            GameManager.instance.LimbData.NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
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
                GameManager.instance.LimbData.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameManager.instance.LimbData.mousePosition.z = 0;
                GameManager.instance.LimbData.attackInput = true;
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
