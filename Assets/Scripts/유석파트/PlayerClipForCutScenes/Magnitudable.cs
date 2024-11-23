using UnityEngine;

public class Magnitudable : MonoBehaviour
{
    public float magnitudeX = 1f; // x축 변화 범위
    public float magnitudeY = 1f; // y축 변화 범위
    public float speed = 2f; // 변화 속도

    private Vector3 originalPosition; // 원래 위치 저장
    private bool isAnimating = false; // 애니메이션 활성화 여부
    private bool isReturning = false; // 원래 위치로 돌아가는 중인지 여부

    void Start()
    {
        // 원래 위치 저장
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isAnimating)
        {
            // Sin 함수를 이용해 x, y 값을 부드럽게 변경
            float offsetX = Mathf.Sin(Time.time * speed) * magnitudeX;
            float offsetY = Mathf.Cos(Time.time * speed) * magnitudeY;
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);
        }
        else if (isReturning)
        {
            // 원래 위치로 부드럽게 복귀
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * speed);

            // 위치가 충분히 가까워지면 복귀 완료
            if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
            {
                transform.position = originalPosition;
                isReturning = false;
            }
        }
    }

    // Magnitude 애니메이션 시작
    public void Magnitude()
    {
        Debug.Log("매그니튜드 시작");
        if(!isAnimating)
        {
            isAnimating = true;
            isReturning = false;
        }
        else
        {
            isAnimating = false;
            isReturning = true;
        }
    }
}
