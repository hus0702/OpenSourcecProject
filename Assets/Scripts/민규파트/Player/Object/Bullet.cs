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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Blind")
        {
            Debug.Log("�ѿ� ����");
            collision.gameObject.GetComponent<Player>().TakingDamage(2);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            //Boss TakeDamage
            Destroy(gameObject);
        }

    }
}
