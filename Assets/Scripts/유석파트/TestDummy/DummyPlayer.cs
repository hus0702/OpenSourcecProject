using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.position = new Vector3(transform.position.x + -2 * Time.deltaTime , transform.position.y , transform.position.z);
    }
}
