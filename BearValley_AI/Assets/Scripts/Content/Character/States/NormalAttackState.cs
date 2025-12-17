using UnityEngine;

public class NormalAttackState : State
{

    public override void Enter()
    {
        Debug.Log("Normal Attack Start");
    }

    public override void Update()
    {
        // 공격 애니메이션 및 판정
    }

    public override void Exit()
    {
        Debug.Log("Normal Attack End");
    }
}
