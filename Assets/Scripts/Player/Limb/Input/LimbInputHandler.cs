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
        #endregion
        #region Interact
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        #endregion
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

    public IEnumerator stopshotinput(float duration)
    {
        isshotblocked = true;
        yield return new WaitForSeconds(duration);

        isshotblocked = false;
    }
}
