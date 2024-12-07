using UnityEngine;

public class Presser : MonoBehaviour
{
    public GameObject killChecker;
    public float rayLength; // 사망 판정을 내릴 수 있는 길이
    public LayerMask platformLayer;
    public float slowSpeed = 0.2f;
    public float fastSpeed = 2f;
    private RaycastHit2D hitInfo;
    private bool isKillable = false;
    private bool isSlowMoving = false;
    private bool isFastMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SlowStart()
    {
        isSlowMoving = true;
    }
    public void FastStart()
    {
        isFastMoving = true;
    }

    public void Stop()
    {
        isSlowMoving = false;
        isFastMoving = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isSlowMoving)
        {
            transform.Translate(Vector2.down * slowSpeed * Time.deltaTime);
        }
        else if(isFastMoving)
        {
            transform.Translate(Vector2.down * fastSpeed * Time.deltaTime);
        }
        if(Physics2D.Raycast(killChecker.transform.position, Vector2.down, 0.1f, platformLayer).collider != null) Stop();

        hitInfo = Physics2D.Raycast(killChecker.transform.position, Vector2.down, rayLength, platformLayer);
        isKillable = hitInfo.collider != null;

        if(isKillable)
        {
            Debug.Log("Kill Player");
            rayLength = 0;
        } 
    }

    public void KillPlayer()
    {
        // 걍 죽이기
        Debug.Log("플레이어를 죽였습니다!");
    }

    void OnDrawGizmos()
    {
        // 기즈모 색상 설정
        Gizmos.color = Color.green;

        // 기즈모로 레이 시각화
        Gizmos.DrawLine(killChecker.transform.position, killChecker.transform.position + Vector3.down * rayLength);
    }
}
