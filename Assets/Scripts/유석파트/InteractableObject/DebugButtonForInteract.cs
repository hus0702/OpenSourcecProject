using UnityEngine;

public class DebugButtonForInteract : MonoBehaviour
{
    public InteractableObject objectToInteract;
    public GameObject dummyPlayer;

    public void Interact()
    {
        objectToInteract.Interact(dummyPlayer);
    }
}
