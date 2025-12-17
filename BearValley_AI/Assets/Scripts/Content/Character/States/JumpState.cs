using UnityEngine;

public class JumpState : State
{
    public override void Enter()
    {
        Debug.Log("Jump Start");
    }

    public override void Update()
    {
        // 피격 처리
    }

    public override void Exit()
    {
        Debug.Log("Jump End");
    }
}
