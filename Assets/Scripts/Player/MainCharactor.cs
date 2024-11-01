using UnityEngine;

public class MainCharactor : MonoBehaviour
{
    
    private int health;
    private float velocity;
    public int Health
    {
        get { return health; }
    }

    public void SetHealth(int value)
    {
        if (value >= 0) 
        {
            health = value;
        }
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
