using UnityEngine;

public class ColliderOverlapTrigger : Trigger
{
    bool isEntering = false;

    Collider2D ownCollider;
    Collider2D[] colliders;
    void Awake()
    {
        ownCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(ownCollider.bounds.center, ownCollider.bounds.size, 0);

        bool isDetected = false;

        foreach(Collider2D colliderItem in colliders)
        {
            if(colliderItem != ownCollider)
            {
                isDetected = true;
            }
        }

        if(isDetected)
        {
            if(!isEntering)
            {
                isEntering = true;
                ActiveTrigger();
            }
        }
        
        else
        {
            isEntering = false;
        }
    }
}
