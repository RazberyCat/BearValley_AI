using UnityEngine;

public class HitState : State
{
    public override void Enter()
    {
        Debug.Log("Hit Start");
    }

    public override void Update()
    {
        // 피격 처리
    }

    public override void Exit()
    {
        Debug.Log("Hit End");
    }
}
