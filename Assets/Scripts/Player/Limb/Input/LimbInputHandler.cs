using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LimbInputHandler : MonoBehaviour
{

    public bool carryinputblock;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    float throwinputtime;
    public bool JumpInput { get; private set; }

    public bool SitInput { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.right).normalized.y;
    }

    public void OnEscInput(InputAction.CallbackContext context)
    {

    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {

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
