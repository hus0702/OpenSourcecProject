using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class LimbShotState : LimbAbillityState
{
    public Vector3 mousePosition;
    public Vector3 bulletrotation;
    public LimbShotState(Limb Limb, PlayerStateMachine stateMachine, LimbData limbdata, string animBoolName) : base(Limb, stateMachine, limbdata, animBoolName)
    {

    }

    public override void Enter()
    {
        mousePosition = Limb.InputHandler.mousePosition;
        bulletrotation = mousePosition - Limb.transform.position;
        var bullet = Limb.Instantiate(Limb.BulletPrefab, Limb.transform.position + bulletrotation.normalized, Quaternion.Euler(bulletrotation));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = (bulletrotation).normalized * limbdata.bulletspeed;
        Limb.InputHandler.StartCoroutine(Limb.InputHandler.stopshotinput(GameManager.instance.LimbData.ShotDelay));
        isAbillityDone = true;
    }


}
