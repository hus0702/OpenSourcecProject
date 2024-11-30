using UnityEngine;

public class ColliderOverlapInteractable : InteractableObject
{
    private void OnCollisionEnter2D(Collision2D other) {
        // 자동으로 상호작용이 발동되어야 함.
        //if (player)
        Interact(other.gameObject);
    }
}
