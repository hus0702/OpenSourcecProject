using Mirror;
using Mirror.Examples.Basic;
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
                limb.CmdSetNormInputX(-1);
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
                    limb.CmdSetNormInputX(0);
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
                limb.CmdSetNormInputX(1);
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
                limb.CmdSetNormInputX(0);
            }
        }
        #endregion
        #region shot
        if (!isshotblocked)
        {
            if (Input.GetMouseButtonDown(1))
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
        if (Input.GetMouseButtonUp(1))
        {
            if (isServer)
            {
                container.attackInput = false;
            }
            else
            {
                limb.CmdSetattackInput(false);
            }
        }
        #endregion
        #region Interact
        if (container.Interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isServer)
                {
                    container.InteractInput = true;
                }
                else
                {
                    limb.CmdSetInteractInput(true);
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isServer)
                {
                    container.InteractInput = false;
                }
                else
                {
                    limb.CmdSetInteractInput(false);
                }
            }
        }
        #endregion
        #region itemScroll

        if (Input.mouseScrollDelta.y > 0)
        {
            limb.changeitem(true);
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            limb.changeitem(false);
        }

        #endregion
    }


    public void OnEscInput(InputAction.CallbackContext context)
    {
        if (isOwned) 
        {
        
        }
    }

    public IEnumerator stopshotinput(float duration)
    {
        isshotblocked = true;
        yield return new WaitForSeconds(duration);

        isshotblocked = false;
    }
}
