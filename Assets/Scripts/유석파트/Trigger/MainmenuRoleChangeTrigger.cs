using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class MainmenuRoleChangeTrigger : ColliderOverlapTrigger
{
    public float fadeDuration;

    public SoundWaveMakable soundMakable;

    public GameObject dummyPlayer;
    public Image fadeImage;


    public GameObject blindMap;
    public GameObject limpMap;
    public bool isBlindMap = false;
    



    public override void ActiveTrigger()
    {
        Color alphaFull = Color.black;
        Color alphaZero = Color.black;
        alphaZero.a = 0;

        StartCoroutine(Fade(alphaZero, alphaFull));
    }

    IEnumerator Fade(Color start, Color end)
    {
        float timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(start, end, timer / fadeDuration);
            yield return null;
        }
        fadeImage.color = end;

        dummyPlayer.transform.position = new Vector3(0f, 8.8f, 0); // 일단 플레이어 이동시키고
        // 맵을 변경한다
        if(isBlindMap)
        {
            blindMap.SetActive(false);
            limpMap.SetActive(true);
        }
        else
        {
            blindMap.SetActive(true);
            limpMap.SetActive(false);
        }
        isBlindMap = !isBlindMap;
        // 발자국 효과가 나오도록 한다.
        //soundMakable.active = isBlindMap;
        SoundWaveManager.Instance.isBlind = isBlindMap;

        yield return new WaitForSeconds(2f);

        timer = 0f;
        while(timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(end, start, timer / fadeDuration);
            yield return null;
        }
        fadeImage.color = start;
    }
}
