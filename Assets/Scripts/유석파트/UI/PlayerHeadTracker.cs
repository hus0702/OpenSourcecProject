using UnityEngine;
using UnityEngine.UI;

public class PlayerHeadTracker : MonoBehaviour
{
    [SerializeField][Header("따라다닐 객체")] private GameObject playerToFollow;
    private RectTransform myTransform;
    public bool isFollow;
    private Vector2 offset; // 머리 위 오프셋
    public float xOffset; // x 좌표 오프셋
    public float yOffset; // y 좌표 오프셋

    private Vector3 screenPosition;


    void Awake()
    {
        myTransform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(isFollow)
        {
            if(playerToFollow == null) Debug.LogError("PlayerHeadTracker " + gameObject.name + ": 따라다닐 목표물이 설정되지 않았습니다!");
            
            offset.x = xOffset; offset.y = yOffset;

            offset.y += myTransform.sizeDelta.y / 2;

            screenPosition = Camera.main.WorldToScreenPoint(playerToFollow.transform.position);
            myTransform.position = new Vector2(screenPosition.x, screenPosition.y) + offset;
        }
    }

    public void FollowMe(GameObject me)
    {
        this.playerToFollow = me;
        isFollow = true;
    }

    public void Follow()
    {
        isFollow = true;
    }
}
