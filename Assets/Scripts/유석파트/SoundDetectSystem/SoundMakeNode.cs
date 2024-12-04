using UnityEngine;
using Mirror;
using UnityEngine.Rendering.Universal;

public class SoundMakeNode : MonoBehaviour
{
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


    void Awake()
    {
        light2d = GetComponent<Light2D>();
    }

    void InitSoundMakeNode()
    {
        light2d.pointLightInnerRadius = 0;
        light2d.pointLightOuterRadius = 0;
        light2d.intensity = 0;
    }

    public void MakeSound(GameObject sourceOfSound, float power, float duration)
    {

        InitSoundMakeNode();

        // 일단 권장 사항으로 해놓겠습니당
        padeInDuration_goal = duration * padeInRatio;
        padeOutDuration_goal = duration * padeOutRatio;

        padeInDuration = padeInDuration_goal;
        innerGrowingDuration = padeInDuration;
        padeOutDuration = padeOutDuration_goal;

        this.power = power;

        isPadeIning = true;
        light2d.intensity = 1;
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
            light2d.intensity = Mathf.SmoothStep(1, 0, Mathf.Pow((padeOutDuration_goal - padeOutDuration) / padeOutDuration_goal, 0.7f));

            if(padeOutDuration <= 0)
            {
                padeOutDuration = 0; // 일단 안전하게 이 값을 0으로 해줍시다
                isPadeOuting = false;
                // 이 시점에서 모든 작업이 끝날거임. 추가 작업이 필요.
            }       
        }
    }
}
