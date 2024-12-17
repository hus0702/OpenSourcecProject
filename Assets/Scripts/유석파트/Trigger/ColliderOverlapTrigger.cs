using UnityEngine;

public class ColliderOverlapTrigger : Trigger
{

    private void OnTriggerEnter2D(Collider2D other) {
        var otherTag = other.gameObject.tag;
        Debug.Log("콜라이더 침입하긴 함");

        if(otherTag == "Blind" || otherTag == "Limb" || otherTag == "DummyPlayerToDisplay")
        {
            ActiveTrigger();
        }
    }
/*
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
*/
}
