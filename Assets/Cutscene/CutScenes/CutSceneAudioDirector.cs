using UnityEngine;

public class CutSceneAudioDirector : MonoBehaviour
{

    public GameObject Blind;
    public GameObject BlindFoot;
    public GameObject Limp;
    public GameObject EmployeeWithGun;
    public GameObject EmployeeFoot;
    public GameObject Researcher;
    public GameObject Door;

    private Vector3 blindPos;
    private Vector3 blindFootPos;
    private Vector3 limpPos;
    private Vector3 employeeWithGunPos;
    private Vector3 employeeFootPos;
    private Vector3 researcherPos;
    private Vector3 doorPos;


    void Update()
    {
        if(Blind != null) blindPos = Blind.transform.position;
        if(BlindFoot != null) blindFootPos = BlindFoot.transform.position;

        if(Limp != null) limpPos = Limp.transform.position;
        
        if(EmployeeWithGun != null) employeeWithGunPos = EmployeeWithGun.transform.position;
        if(EmployeeFoot != null) employeeFootPos = EmployeeFoot.transform.position;
        
        if(Researcher != null) researcherPos = Researcher.transform.position;

        if(Door != null) doorPos = Door.transform.position;
    }

    public void PlayBlindWalk()
    {
        SoundWaveManager.Instance.MakeSoundWave((int)AudioManager.Sfx.Blindwalk, false, blindFootPos, 4f, 0.8f);
    }
    public void PlayBlindLand()
    {
        SoundWaveManager.Instance.MakeSoundWave((int)AudioManager.Sfx.BlindLand, false, blindFootPos, 4f, 0.8f);
    }
    public void PlayBlindBadLand()
    {
        SoundWaveManager.Instance.MakeSoundWave((int)AudioManager.Sfx.BlindLand, false, blindPos, 4f, 0.8f);
    }
    public void PlayBlindTalk()
    {
        SoundWaveManager.Instance.MakeSoundWave(-1, false, blindPos, 4f, 0.8f);
    }
    public void PlayBlindShout()
    {
        SoundWaveManager.Instance.MakeSoundWave(-1, false, blindPos, 6f, 0.8f);
    }





    public void PlayLimpTalk()
    {
        SoundWaveManager.Instance.MakeSoundWave(-1, false, limpPos, 4f, 0.8f);
    }
    public void PlayLimpShout()
    {
        SoundWaveManager.Instance.MakeSoundWave(-1, false, limpPos, 6f, 0.8f);
    }




    public void PlayOpenDoor()
    {
        SoundWaveManager.Instance.MakeSoundWave((int)AudioManager.Sfx.opendoor, false, doorPos, 7f, 0.8f);
    }
    public void PlayEmpWalk()
    {
        SoundWaveManager.Instance.MakeSoundWave((int)AudioManager.Sfx.Blindwalk, false, employeeFootPos, 4f, 0.8f);
    }


}
