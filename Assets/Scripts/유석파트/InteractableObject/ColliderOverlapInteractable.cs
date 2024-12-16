using UnityEngine;

public class ColliderOverlapInteractable : InteractableObject
{
    private void OnTriggerEnter2D(Collider2D other) {
        // 자동으로 상호작용이 발동되어야 함.
        //if (player)
        Debug.Log(gameObject.name + "과 상호작용을 시작합니다.");
        Interact(other.gameObject);
    }
}
