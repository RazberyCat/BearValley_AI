using UnityEngine;

public class StrongAttackState : State
{

    public override void Enter()
    {
        Debug.Log("Strong Attack Start");
    }

    public override void Update()
    {
        // 강공격 처리
    }

    public override void Exit()
    {
        Debug.Log("Strong Attack End");
    }
}
