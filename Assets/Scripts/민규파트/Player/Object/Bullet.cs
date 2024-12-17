using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 2.0f);
        Debug.Log(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Blind")
        {
            this.gameObject.GetComponent<Player>().TakingDamage(5);
        }

        if (collision.gameObject.tag == "Boss")
        {
            //Boss TakeDamage
            
        }
        Destroy(gameObject);

    }
}
