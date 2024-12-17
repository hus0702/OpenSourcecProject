using UnityEngine;
using Mirror;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SoundMakeNode : MonoBehaviour
{
    /*
        여기에 소리를 재생하는 코드를 넣으면 됩니다. 조건 검사는 이미 OnTriggerEnter2D() 함수에서 마쳤습니다.
    
        인자는 주시는 대로 주시면 받는 대로 받아 PlaySound 함수를 수정하겠습니다!
    */
    public void PlaySound()
    {
        Debug.Log("소리를 재생합니다!");

        AudioManager.instance.PlaySfx((AudioManager.Sfx)sfxToPlay); // 일단 이렇게 넣어봤음.
    }


    public int sfxToPlay;
    public bool isShouldCheckCollider;


    private ObjectPool returnPool; // 만약 이벤트가 모두 끝나고 돌아가야 할 오브젝트 풀이 있다면 이 곳에 저장된다!

    private Light2D light2d;

    private float padeInDuration_goal;
    private float remainDuration_goal;
    private float padeOutDuration_goal;

    private float padeInDuration;
    private float innerGrowingDuration;
    private float padeOutDuration;

    private float padeInRatio = 0.8f;
    private float remainRatio = 0.0f;
    private float padeOutRatio = 1f;

    private bool isPadeIning = false;
    private bool isInnerGrowing = false;
    private bool isPadeOuting = false;

    private float power;



    private bool isLigitOff;


    void Awake()
    {
        light2d = GetComponent<Light2D>();
    }

    void InitSoundMakeNode()
    {
        light2d.gameObject.SetActive(true);
        light2d.pointLightInnerRadius = 0;
        light2d.pointLightOuterRadius = 0;
        light2d.intensity = 0;
    }

    public void setMyPool(ObjectPool pool)
    {
        returnPool = pool;
    }

    public void MakeSound(int sfxNum, bool isShouldCheckCollider, Vector3 sourceOfSound, float power, float duration)
    {
        sfxToPlay = sfxNum;
        this.isShouldCheckCollider = isShouldCheckCollider;

        InitSoundMakeNode();

        if(transform == null)
        {
            Debug.LogError("노드의 transform 이 null 입니다!!!");
        }
        else if(sourceOfSound == null)
        {
            Debug.LogError("소리를 낼 객체가 할당되지 않았습니다!!");
        }
        transform.position = sourceOfSound;

        // 일단 권장 사항으로 해놓겠습니당
        padeInDuration_goal = duration * padeInRatio;
        padeOutDuration_goal = duration * padeOutRatio;

        padeInDuration = padeInDuration_goal;
        innerGrowingDuration = padeInDuration;
        padeOutDuration = padeOutDuration_goal;

        this.power = power;

        isPadeIning = true;
        light2d.intensity = 0.8f;
        
        if(!SoundWaveManager.Instance.isBlind)
        {
            isLigitOff = true;
            light2d.intensity = 0.0f;
        }
        else
        {
            isLigitOff = false;
        }
        
        if(!isShouldCheckCollider) PlaySound();
    }

    public void Update()
    {
        if(isPadeIning)
        {
            padeInDuration -= Time.deltaTime;
            light2d.pointLightOuterRadius = Mathf.SmoothStep(0, power, (padeInDuration_goal - padeInDuration) / padeInDuration_goal);

            if(padeInDuration <= padeInDuration_goal)
            {
                isPadeOuting = true;
            }

            if(padeInDuration <= padeInDuration_goal / 5 * 4)
            {
                isInnerGrowing = true;
            }

            if(padeInDuration <= 0)
            {
                padeInDuration = 0; // 일단 안전하게 이 값을 0으로 해줍시다
                isPadeIning = false;
                //isRemaining = true;
            }
        }

        if(isInnerGrowing)
        {
            innerGrowingDuration -= Time.deltaTime;
            light2d.pointLightInnerRadius = Mathf.SmoothStep(0, power * 0.8f, (padeInDuration_goal - innerGrowingDuration) / padeInDuration_goal);
            if(innerGrowingDuration <= 0)
            {
                innerGrowingDuration = 0; // 일단 안전하게 이 값을 0으로 해줍시다
                isInnerGrowing = false;
                //isRemaining = true;
            }
        }

        if(isPadeOuting)
        {
            padeOutDuration -= Time.deltaTime;
            if(!isLigitOff) light2d.intensity = Mathf.SmoothStep(0.8f, 0, Mathf.Pow((padeOutDuration_goal - padeOutDuration) / padeOutDuration_goal, 0.2f));

            if(padeOutDuration <= 0)
            {
                padeOutDuration = 0; // 일단 안전하게 이 값을 0으로 해줍시다
                isPadeOuting = false;
                // 이 시점에서 모든 작업이 끝날거임. 추가 작업이 필요.

                if(returnPool != null)
                {
                    returnPool.ReleaseObject(gameObject);
                }
            }       
        }
    }

    private void OnTriggerEnter2D(Collider2D detectedCollider)
    {
        if(!isShouldCheckCollider)
        {
            return;
        }

        var gameObjectOfColliderTag = detectedCollider.gameObject.tag;
    
        if(gameObjectOfColliderTag == "Blind" || gameObjectOfColliderTag == "Limb")
        {
            // 일단 플레이어를 감지했다는건 로컬 플레이어를 확인할 수 있는 상태가 됐단거임.
            var localPlayer = NetworkClient.localPlayer.GetComponent<PlayerObjectController>();

            if((gameObjectOfColliderTag == "Blind" && localPlayer.Role == PlayerObjectController.Blind)
                || (gameObjectOfColliderTag == "Limb" && localPlayer.Role == PlayerObjectController.Limp))
            {
                PlaySound();
            }
        }
    }
}
