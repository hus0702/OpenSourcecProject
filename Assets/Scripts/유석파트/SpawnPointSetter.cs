using UnityEngine;

public class SpawnPointSetter : ColliderOverlapInteractable
{
    public override void ExecuteOnSuccess(GameObject requester)
    {
        Debug.Log("스폰 포인트가 갱신됐습니다!");


        GameManager.instance.BlindSpawnPositionOnLoad = new Vector3(-4, 0.5f, 0);
        GameManager.instance.LimpSpawnPositionOnLoad = new Vector3(-4, 0.5f, 0);


        base.ExecuteOnSuccess(requester);
    }
}
